import { combineReducers } from 'redux';
import weatherGeocodingReducer from './weatherGeocoding/reducer';

const rootReducer = combineReducers({
  weatherGeocoding: weatherGeocodingReducer,
  //add other reducers below
});

export default rootReducer;
