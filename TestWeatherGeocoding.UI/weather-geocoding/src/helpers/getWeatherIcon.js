import { faSun, faCloud, faCloudShowersHeavy, faCloudRain, faBolt, faSnowflake } from '@fortawesome/free-solid-svg-icons';

export function getWeatherIcon(description) {
    switch (description.toLowerCase()) {
        case 'sunny':
            return faSun;
        case 'partly cloudy':
            return faCloud;
        case 'cloudy':
            return faCloud;
        case 'rainy':
            return faCloudShowersHeavy;
        case 'showers':
            return faCloudRain;
        case 'clear':
            return faSun;
        case 'thunderstorm':
            return faBolt;
        case 'snow':
            return faSnowflake;
        default:
            return null;
    }
};

