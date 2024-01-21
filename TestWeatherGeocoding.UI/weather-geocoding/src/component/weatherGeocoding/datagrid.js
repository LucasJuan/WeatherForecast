import React from 'react';
import { useSelector } from 'react-redux';
import Table from 'react-bootstrap/Table';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { getWeatherIcon, getWeatherDescription } from "../../helpers/getWeatherIcon";

function WeatherGeocodingGrid() {
    const selector = useSelector((state) => state.weatherGeocoding);

    if (!selector.data || !selector.data.daily || !selector.data.daily.weather_code) {
        return <p>No weather data available.</p>;
    }

    return (
        <>
            <div className='pt-3'>
                {selector.data.daily.weather_code && selector.data.daily.weather_code.length > 0 ? (
                    <Table striped bordered hover responsive>
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Temperature Max (°C)</th>
                                <th>Temperature Min (°C)</th>
                                <th className="col-md-3">Description</th>
                            </tr>
                        </thead>
                        <tbody>
                            {selector.data.daily.time.map((date, index) => (
                                <tr key={date}>
                                    <td>{date}</td>
                                    <td>{selector.data.daily.temperature_2m_max && selector.data.daily.temperature_2m_max[index]}</td>
                                    <td>{selector.data.daily.temperature_2m_min && selector.data.daily.temperature_2m_min[index]}</td>
                                    <td>
                                        {selector.data.daily.weather_code[index] && (
                                            <>
                                                <FontAwesomeIcon icon={getWeatherIcon(selector.data.daily.weather_code[index])} size="2x" />
                                                {' '}
                                                {getWeatherDescription(selector.data.daily.weather_code[index])}
                                            </>
                                        )}
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                ) : (
                    <p>No weather data available.</p>
                )}
            </div>
        </>
    );
}

export default WeatherGeocodingGrid;
