import axios from 'axios';

const apiUrl = process.env.REACT_APP_API_URL;

const apiClient = axios.create({
    baseURL: apiUrl,
    headers: {
        'Content-Type': 'application/json'
    }
});

apiClient.interceptors.request.use(config => {

    const token = localStorage.getItem('token');

    if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
    }

    return config;

});

apiClient.interceptors.response.use(
    response => response,
    async error => {
        const originalRequest = error.config;

        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;

            try {

                const refreshToken = localStorage.getItem('refresh_token');

                const response = await apiClient.post('Login/Refresh', {
                    accessToken: localStorage.getItem('accessToken'),
                    refreshToken: refreshToken
                });

                const { accessToken, refreshToken: newRefreshToken } = response.data;

                localStorage.setItem('token', accessToken);
                localStorage.setItem('refresh_token', newRefreshToken);

                originalRequest.headers['Authorization'] = `Bearer ${accessToken}`;

                return axios(originalRequest);

            } catch (err) {
                console.error('Error refreshing token:', err);
            }
        }

        return Promise.reject(error);
    }
);

export default apiClient;