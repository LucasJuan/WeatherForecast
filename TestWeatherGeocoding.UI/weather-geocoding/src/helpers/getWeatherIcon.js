import { faSun, faCloud, faCloudShowersHeavy, faCloudRain, faBolt, faSnowflake } from '@fortawesome/free-solid-svg-icons';

export function getWeatherIcon(code) {
    switch (code) {
        case 0:
        case 1:
        case 2:
        case 3:
            return faSun;
        case 45:
        case 48:
            return faCloud;
        case 51:
        case 53:
        case 55:
            return faCloudRain;
        case 56:
        case 57:
            return faCloudRain; // Modify as needed for freezing drizzle icons
        case 61:
        case 63:
        case 65:
            return faCloudShowersHeavy;
        case 66:
        case 67:
            return faCloudShowersHeavy; // Modify as needed for freezing rain icons
        case 71:
        case 73:
        case 75:
            return faSnowflake;
        case 77:
            return faSnowflake; // Modify as needed for snow grains icon
        case 80:
        case 81:
        case 82:
            return faCloudRain; // Modify as needed for rain showers icons
        case 85:
        case 86:
            return faSnowflake;
        case 95:
        case 96:
        case 99:
            return faBolt;
        default:
            return null;
    }
}

export function getWeatherDescription(code) {
    switch (code) {
        case 0:
        case 1:
        case 2:
        case 3:
            return 'Clear sky';
        case 45:
        case 48:
            return 'Fog and depositing rime fog';
        case 51:
        case 53:
        case 55:
            return 'Drizzle';
        case 56:
        case 57:
            return 'Freezing Drizzle';
        case 61:
        case 63:
        case 65:
            return 'Rain';
        case 66:
        case 67:
            return 'Freezing Rain';
        case 71:
        case 73:
        case 75:
            return 'Snow fall';
        case 77:
            return 'Snow grains';
        case 80:
        case 81:
        case 82:
            return 'Rain showers';
        case 85:
        case 86:
            return 'Snow showers';
        case 95:
        case 96:
        case 99:
            return 'Thunderstorm';
        default:
            return null;
    }
}