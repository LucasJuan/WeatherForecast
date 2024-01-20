import { useSelector } from 'react-redux';
import Table from 'react-bootstrap/Table';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { getWeatherIcon } from "../../helpers/getWeatherIcon";

function WeatherGeocodingGrid() {
    const selector = useSelector((state) => state.weatherGeocoding);
    return (
        <>
            <div className='pt-3'>
                {selector.data ? (
                    selector.data.length > 0 ? (
                        <Table striped bordered hover responsive>
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Temperature (°C)</th>
                                    <th className="col-md-2">Description</th>
                                </tr>
                            </thead>
                            <tbody>
                                {selector.data.map(dayForecast => (
                                    <tr key={dayForecast.date}>
                                        <td>{dayForecast.date}</td>
                                        <td>{dayForecast.temperature}</td>
                                        <td>
                                            <FontAwesomeIcon icon={getWeatherIcon(dayForecast.description)} size="2x" /> {dayForecast.description}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </Table>
                    ) : (
                        <p>{selector.error}</p>
                    )
                ) : (
                    <p>Loading...</p>
                )}
            </div>
        </>
    );
}

export default WeatherGeocodingGrid;
