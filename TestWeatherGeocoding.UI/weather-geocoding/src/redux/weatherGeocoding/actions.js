import instance from '../../helpers/axios/config'
import weatherGeocodingActions from './actionsType';

export const getData = (data) => async (dispatch) => {
    try {
        debugger
        const encodedAddress = encodeURIComponent(data);
        const response = await instance.get(`GetWeatherForecast?address=${encodedAddress}`);
        if (response && response.data && response.data.success) {
            await dispatch({
                type: weatherGeocodingActions.FETCH_DATA,
                data: response.data.data,
            });
        } else {
            await dispatch({
                type: weatherGeocodingActions.ERROR_FETCH,
                data: response.data.message,
            });
        }
    } catch (error) {
        console.log(error)
        await dispatch({
            type: weatherGeocodingActions.ERROR_FETCH,
            data: error.message,
        });
    }
};