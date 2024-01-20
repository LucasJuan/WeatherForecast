import React from "react";
import { Provider } from "react-redux";
import WeatherGeocoding from "./component/weatherGeocoding";
import store from "./redux/store";
import CustomSpinner from "./component/CustomSpinner";

function App() {
  return (
    <div className="App container p-2">
      <Provider store={store}>
        <CustomSpinner/>
        <WeatherGeocoding />
      </Provider>
    </div>
  );
}

export default App;