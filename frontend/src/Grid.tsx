import {useEffect, useState} from "react";

interface WeatherEntry {
    id: string;
    temperatureC: number,
    summary: string,
    date: string
}

function Grid() {
    const [weather, setWeather] = useState<WeatherEntry[]>([])

    useEffect(() => {
        const fetchWeather = async () => {
            const response = await fetch('/api/WeatherForecast')
            const data = await response.json()
            console.log(data)
            setWeather(data)
        }

        fetchWeather().catch(console.error)
    }, []);

    return (
        <table>
            <thead>
            <tr key="head">
                <th>Date</th>
                <th>Summary</th>
                <th>Temp C</th>
            </tr>
            </thead>
            <tbody>
            {weather && weather.map(weatherEntry =>
                <tr key={weatherEntry.id}>
                    <td>{weatherEntry.date}</td>
                    <td>{weatherEntry.summary}</td>
                    <td>{weatherEntry.temperatureC}</td>
                </tr>
            )}
            </tbody>
        </table>
    )
}

export default Grid