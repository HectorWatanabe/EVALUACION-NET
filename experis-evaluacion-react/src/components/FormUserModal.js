import { Button, DatePicker, Form, Input, Modal, Select, Switch, message } from "antd";
import { storeUser, updateUser } from "../services/UserServices";
import { useEffect } from "react";
import dayjs from "dayjs";

const FormUserModal = (props) => {

    const { isModalOpen, handleCancel, getUsersHandler, userTemp } = props;

    const [form] = Form.useForm();

    const onFinish = (values) => {

        const { email, password, fechaNacimiento, roles, genero, bloquear } = values;

        if (userTemp === null) {
            storeUser(email, password, fechaNacimiento, genero, roles, bloquear).then(response => {
                message.success('Se registró el usuario.', 3);
                getUsersHandler();
                handleCancel();
                form.resetFields();
            }).catch(error => {
                message.error('Ocurrió un error.');
            });
        } else {
            updateUser(email, password, fechaNacimiento, genero, roles, bloquear, userTemp).then(response => {
                message.success('Se actualizó el usuario.', 3);
                getUsersHandler();
                handleCancel();
                form.resetFields();
            }).catch(error => {
                message.error('Ocurrió un error.');
            });
        }

    };

    useEffect(() => {
        if (userTemp !== null) {
            form.setFieldsValue({
                email: userTemp.email,
                fechaNacimiento: dayjs(userTemp.fechaNacimiento),
                roles: userTemp.roles.split(", "),
                genero: userTemp.genero,
                bloquear: userTemp.bloquear,
            });
        }
    }, [userTemp]);

    return (
        <Modal title="Formulario de usuario" open={isModalOpen} onCancel={() => {
            handleCancel();
            form.resetFields();
        }} footer={null}>
            <Form form={form} onFinish={onFinish} layout="vertical">
                <Form.Item
                    label="Email:"
                    name="email"
                    rules={[
                        {
                            required: true,
                            message: 'Por favor, ingresa un email.',
                        },
                    ]}
                >
                    <Input style={{ width: '100%' }} />
                </Form.Item>
                <Form.Item
                    label="Contraseña:"
                    name="password"
                    rules={[
                        {
                            required: true,
                            message: 'Por favor, ingresa una contraseña.',
                        },
                    ]}
                >
                    <Input.Password style={{ width: '100%' }} />
                </Form.Item>
                <Form.Item
                    label="Fecha de nacimiento: "
                    name="fechaNacimiento"
                    rules={[
                        {
                            required: true,
                            message: 'Por favor, ingresa una fecha de nacimiento.',
                        },
                    ]}
                >
                    <DatePicker style={{ width: '100%' }} format="DD-MM-YYYY" />
                </Form.Item>
                <Form.Item
                    label="Roles:"
                    name="roles"
                    rules={[
                        {
                            required: true,
                            message: 'Por favor, selecciona uno o más roles.',
                        },
                    ]}
                >
                    <Select
                        mode="tags"
                        allowClear
                        style={{
                            width: '100%',
                        }}
                        placeholder="Selecciona uno o más roles"
                        options={[
                            {
                                label: 'Administrador',
                                value: 'Administrador',
                            },
                            {
                                label: 'Supervisor',
                                value: 'Supervisor',
                            },
                            {
                                label: 'Colaborador',
                                value: 'Colaborador',
                            }
                        ]}
                    />
                </Form.Item>
                <Form.Item
                    label="Genero:"
                    name="genero"
                    rules={[
                        {
                            required: true,
                            message: 'Por favor, selecciona un genero.',
                        },
                    ]}
                >
                    <Select
                        style={{
                            width: '100%',
                        }}
                        options={[
                            {
                                value: 'm',
                                label: 'Masculino',
                            },
                            {
                                value: 'f',
                                label: 'Femenino',
                            },
                            {
                                value: 'o',
                                label: 'Otros',
                            },
                        ]}
                    />
                </Form.Item>
                <Form.Item
                    label="Bloquear:"
                    name="bloquear"
                    valuePropName="checked"
                >
                    <Switch />
                </Form.Item>
                <div className="RightItem">
                    <Button type="primary" htmlType="submit">
                        Enviar
                    </Button>
                </div>
            </Form>
        </Modal>
    );

};

export default FormUserModal;