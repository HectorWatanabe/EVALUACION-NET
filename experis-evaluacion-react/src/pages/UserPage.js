import { useEffect, useState } from "react";
import { deleteUser, getUsers } from "../services/UserServices";
import UserTable from "../components/UserTable";
import { Button, message } from "antd";
import FormUserModal from "../components/FormUserModal";
import { useNavigate } from "react-router-dom";

const UserPage = () => {

    const navigate = useNavigate();

    const [dataSource, setData] = useState([]);
    const [userTemp, setUserTemp] = useState(null);

    const [isModalOpen, setIsModalOpen] = useState(false);

    const showModal = () => {
        setIsModalOpen(true);
    };

    const editModal = (record) => {
        setUserTemp(record);
        setIsModalOpen(true);
    };

    const handleCancel = () => {
        setUserTemp(null);
        setIsModalOpen(false);
    };

    const getUsersHandler = () => {

        getUsers().then(response => {
            setData(response.data);
        });

    };

    const deleteUserHandler = (record) => {
        deleteUser(record).then(response => {
            getUsersHandler();
            message.success('Se eliminó el registro.');
        }).catch(error => {
            console.log('Ocurrio un error');
        });
    }

    useEffect(() => {

        if (localStorage.getItem('token') === null) {
            navigate("/");
            return;
        }

        getUsersHandler();

    }, []);

    return (
        <div className="UsePageComponent">
            <div className="mb-2">
                <h1>Lista de usuarios</h1>
            </div>
            <div className="mb-2 space">
                <Button onClick={showModal}>Crear Usuario</Button>
                <Button onClick={() => {
                    localStorage.removeItem('token');
                    localStorage.removeItem('refresh_token');
                    localStorage.removeItem('email');
                    navigate('/');
                }}>Cerrar Sesión</Button>
            </div>
            <UserTable dataSource={dataSource} showModal={editModal} deleteUserHandler={deleteUserHandler} />
            <FormUserModal userTemp={userTemp} isModalOpen={isModalOpen} handleCancel={handleCancel} getUsersHandler={getUsersHandler} />
        </div>
    );

};

export default UserPage;