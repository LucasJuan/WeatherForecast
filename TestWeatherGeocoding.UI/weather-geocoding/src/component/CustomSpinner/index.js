import { useState, useEffect } from "react";
import instance from "../../helpers/axios/config";
import { Spinner } from 'react-bootstrap';
import "./Spinner.css";

function CustomSpinner() {
    const [isLoading, setIsLoading] = useState(false);
    useEffect(() => {
        const requestInterceptor = instance.interceptors.request.use(
            (config) => {
                setIsLoading(true);
                return config;
            },
            (error) => {
                setIsLoading(false);
                return Promise.reject(error);
            }
        );

        const responseInterceptor = instance.interceptors.response.use(
            (response) => {
                setIsLoading(false);
                return response;
            },
            (error) => {
                setIsLoading(false);
                return Promise.reject(error);
            }
        );

        return () => {
            instance.interceptors.request.eject(requestInterceptor);
            instance.interceptors.response.eject(responseInterceptor);
        };
    }, []);

    return (
        <>
            {isLoading && (
                <div className="spinner-overlay d-flex flex-column align-items-center justify-content-center">
                    <Spinner animation="border" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </Spinner>
                    <p className="text-white pt-4">Loading...</p>
                </div>
            )}
        </>
    );
}

export default CustomSpinner;
