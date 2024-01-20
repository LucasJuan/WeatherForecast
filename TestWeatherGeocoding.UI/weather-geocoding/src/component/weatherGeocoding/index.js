import { useState } from "react";
import { useDispatch } from "react-redux";
import { getData } from "../../redux/weatherGeocoding/actions";
import WeatherGeocodingGrid from "./datagrid";

function WeatherGeocoding() {
  const dispatch = useDispatch();
  const [address, setAddress] = useState("");

  const getWeatherForecast = async () => {
    await dispatch(getData(address));
  };

  return (
    <div className="container mt-5 text-center">
      <div className="row justify-content-center">
      <h5>Lucas Juan - Test to display 7 day forecast for a specified postal address</h5>
        <div className="col-md-6 pt-2">
          <div className="input-group">
            <input
              type="text"
              value={address}
              className="form-control"
              placeholder="Ex: 4600 Silver Hill Rd, Washington, DC 20233"
              onChange={(e) => setAddress(e.target.value)}
            />
            <button
              className="btn btn-success ml-2"
              onClick={getWeatherForecast}
            >
              Get Forecast
            </button>
          </div>
          <WeatherGeocodingGrid />
        </div>
      </div>
    </div>
  );
}

export default WeatherGeocoding;
