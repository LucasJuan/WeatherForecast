import weatherGeocodingActions from "./actionsType";
const initialState = {
  data: {},
  error:"No content to show"
};

function weatherGeocodingReducer(state = initialState, action) {
  switch (action.type) {
    case weatherGeocodingActions.FETCH_DATA:
      return {
        ...state,
        data: action.data ??{},
      };
      case weatherGeocodingActions.ERROR_FETCH:
      return {
        ...state,
        error: action.data ?? "",
      };
    default:
      return state;
  }
}
export default weatherGeocodingReducer;