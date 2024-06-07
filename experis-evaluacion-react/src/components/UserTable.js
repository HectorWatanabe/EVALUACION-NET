import { Button, Popconfirm, Space, Table, Tag } from "antd";
import dayjs from "dayjs";
import { deleteUser } from "../services/UserServices";

const UserTable = (props) => {

    const { dataSource, showModal, deleteUserHandler } = props;

    const columns = [
        {
            title: 'Email',
            dataIndex: 'email',
            key: 'email',
        },
        {
            title: 'Genero',
            dataIndex: 'genero',
            key: 'genero',
            render: (_, record) => {

                if (record.genero === 'm') {
                    return (<div>Masculino</div>);
                }

                if (record.genero === 'f') {
                    return (<div>Femenino</div>);
                }

                if (record.genero === 'o') {
                    return (<div>Otro</div>);
                }

                return <></>

            }
        },
        {
            title: 'Roles',
            dataIndex: 'roles',
            key: 'roles',
        },
        {
            title: 'Marca temporal de creación',
            dataIndex: 'marcaTemporalCreacion',
            key: 'marcaTemporalCreacion',
            render: (_, record) => {
                const fechaFormateada = dayjs(record.marcaTemporalCreacion).format('DD MMMM YYYY, HH:mm:ss');
                return <>{fechaFormateada}</>;
            }
        },
        {
            title: 'Bloqueado',
            dataIndex: 'bloquear',
            key: 'bloquear',
            render: (_, record) => {
                return <>{record.bloquear ?
                    <Tag color={'green'} key={1}>
                        Si
                    </Tag> :
                    <Tag color={'red'} key={2}>
                        No
                    </Tag>
                }</>
            }
        },
        {
            title: 'Opciones',
            key: 'opciones',
            render: (_, record) => {
                return (<>
                    <Space>
                        <Button onClick={() => { showModal(record) }}>Editar</Button>
                        <Popconfirm
                            title="¿Seguro quiere eliminar el registro?"
                            onConfirm={() => { deleteUserHandler(record) }}
                            okText="Si"
                            cancelText="No"
                        ><Button>Eliminar</Button></Popconfirm>
                    </Space>
                </>)
            }
        }
    ];

    return (<Table dataSource={dataSource} columns={columns} rowKey="id" />);

};

export default UserTable;