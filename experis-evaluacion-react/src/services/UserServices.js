import dayjs from 'dayjs';
import apiClient from "../helpers/ApiClientHelper";

export const getUsers = () => apiClient.get('Usuarios');

export const storeUser = (email, password, fechaNacimiento, genero, roles, bloquear) => {

    return apiClient.post('Usuarios', {
        "id": 0,
        "email": email,
        "password": password,
        "fechaNacimiento": fechaNacimiento.toISOString(),
        "genero": genero,
        "roles": roles.join(", "),
        "bloquear": bloquear,
        "marcaTemporalCreacion": dayjs().toISOString(),
        "usuarioCreador": localStorage.getItem('email'),
        "usuarioActualizador": "",
        "usuarioEliminador": "",
        "estadoEliminado": false
    });

};

export const updateUser = (email, password, fechaNacimiento, genero, roles, bloquear, userTemp) => {

    return apiClient.put(`Usuarios/${userTemp.id}`, {
        "id": userTemp.id,
        "email": email,
        "password": password,
        "fechaNacimiento": fechaNacimiento.toISOString(),
        "genero": genero,
        "roles": roles.join(", "),
        "bloquear": bloquear,
        "marcaTemporalCreacion": userTemp.marcaTemporalCreacion,
        "usuarioCreador": userTemp.usuarioCreador,
        "marcaTemporalActualizacion": dayjs().toISOString(),
        "usuarioActualizador": localStorage.getItem('email'),
        "usuarioEliminador": "",
        "estadoEliminado": false,
    });

};

export const deleteUser = (userTemp) => {

    return apiClient.put(`Usuarios/${userTemp.id}`, {
        "id": userTemp.id,
        "email": userTemp.email,
        "password": userTemp.password,
        "fechaNacimiento": userTemp.fechaNacimiento,
        "genero": userTemp.genero,
        "roles": userTemp.roles,
        "bloquear": userTemp.bloquear,
        "marcaTemporalCreacion": userTemp.marcaTemporalCreacion,
        "usuarioCreador": userTemp.usuarioCreador,
        "marcaTemporalActualizacion": userTemp.marcaTemporalActualizacion,
        "usuarioActualizador": userTemp.usuarioActualizador,
        "marcaTemporalEliminado": dayjs().toISOString(),
        "usuarioEliminador": localStorage.getItem('email'),
        "estadoEliminado": true,
    });

};