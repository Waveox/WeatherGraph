import { Head } from '@inertiajs/react';
import WeatherGraph from '../components/WeatherChart';

const Index = () => {
  return (
    <>
      <Head>
        <title>Weather Dashboard</title>
        <meta name="description" content="View the latest weather data." />
      </Head>
      <div className="flex flex-col items-center justify-center min-h-screen p-4 bg-gray-100">
        <h1 className="text-4xl font-bold text-gray-800 mb-4">Weather Dashboard</h1>
        <div className="w-full max-w-6xl h-auto bg-white shadow-md rounded-lg p-4">
          <WeatherGraph />
        </div>
      </div>
    </>
  );
};

export default Index;