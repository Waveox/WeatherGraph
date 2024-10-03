import React, { useEffect, useState } from 'react';
import { Line } from 'react-chartjs-2';
import { Chart, registerables } from 'chart.js';
import 'chartjs-adapter-date-fns';

Chart.register(...registerables);

interface WeatherData {
  id: string;
  country: string;
  city: string;
  temperature: number;
  minTemperature: number;
  maxTemperature: number;
  created: string;
  updated: string;
}

interface DataPoint {
  x: Date;
  y: number;
}

const fetchWeatherData = async (): Promise<WeatherData[]> => {
  try {
    const response = await fetch('/weather/GetWeatherData');
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching weather data:", error);
    return [];
  }
};

const WeatherGraph = () => {
  const [weatherData, setWeatherData] = useState<WeatherData[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const data = await fetchWeatherData();
      setWeatherData(data);
    };

    loadData();
  }, []);

  if (!weatherData.length) {
    return <div>Loading...</div>;
  }

  const datasets = createDatasets(weatherData);

  const filteredDatasets = filterDatasets(datasets);

  const data = {
    datasets: filteredDatasets,
  };

  const options = getChartOptions(filteredDatasets);

  return (
    <div>
      <Line data={data} options={options} />
    </div>
  );
};

const createDatasets = (weatherData: WeatherData[]) => {
  return weatherData.reduce((acc: any[], data) => {
    const cityKey = `${data.city}, ${data.country}`;

    const cityIndex = acc.findIndex((d) => d.label === cityKey);

    if (cityIndex !== -1) {
      acc[cityIndex].data.push({ x: new Date(data.updated), y: data.temperature } as DataPoint);
    } else {
      acc.push({
        label: cityKey,
        data: [{ x: new Date(data.updated), y: data.temperature }] as DataPoint[],
        borderColor: getRandomColor(),
        backgroundColor: 'rgba(75, 192, 192, 0.2)',
        fill: false,
        minTemperature: data.minTemperature,
        maxTemperature: data.maxTemperature,
      });
    }

    return acc;
  }, []);
};

const filterDatasets = (datasets: any[]) => {
  const cutoffTime = Math.max(...datasets.map(dataset => Math.min(...dataset.data.map((d: { x: { getTime: () => number; }; }) => d.x.getTime()))));

  const oneMinuteCutoffTime = cutoffTime + 60 * 1000;

  return datasets.map(dataset => ({
    ...dataset,
    data: dataset.data.filter((d: DataPoint) => d.x.getTime() >= oneMinuteCutoffTime),
  })).filter(dataset => dataset.data.length > 0);
};

const getChartOptions = (filteredDatasets: any[]) => ({
  responsive: true,
  plugins: {
    legend: {
      position: 'top' as const,
    },
    tooltip: {
      callbacks: {
        label: (context: any) => {
          const label = context.dataset.label || '';
          const value = context.raw.y;
          const timestamp = new Date(context.raw.x).toLocaleString();

          const dataset = filteredDatasets.find(d => d.label === label);
          const minTemp = dataset ? dataset.minTemperature : '';
          const maxTemp = dataset ? dataset.maxTemperature : '';

          return `${label}: ${value}°C at ${timestamp} (Min: ${minTemp}°C, Max: ${maxTemp}°C)`;
        },
      },
    },
  },
  scales: {
    x: {
      type: 'time' as const,
      title: {
        display: true,
        text: 'Time',
      },
      time: {
        unit: 'minute' as const,
        tooltipFormat: 'PPpp',
        displayFormats: {
          minute: 'HH:mm',
        },
      },
    },
    y: {
      title: {
        display: true,
        text: 'Temperature (°C)',
      },
      beginAtZero: true,
    },
  },
});

const getRandomColor = () => {
  const randomInt = () => Math.floor(Math.random() * 255);
  return `rgba(${randomInt()}, ${randomInt()}, ${randomInt()}, 1)`;
};

export default WeatherGraph;
