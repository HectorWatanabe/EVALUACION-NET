import { Button, Card, Form, Input, Spin, message } from "antd";
import { LoginService } from "../services/LoginService";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";

const LoginPage = () => {

    const [loading, setLoading] = useState(false);

    const navigate = useNavigate();

    useEffect(() => {
        if (localStorage.getItem('token') !== null) {
            navigate("/Bienvenido");
        }
    }, []);

    const onFinish = (values) => {

        const { email, password } = values;

        setLoading(true);

        LoginService(email, password).then((response) => {
            localStorage.setItem('token', response.data.accessToken);
            localStorage.setItem('refresh_token', response.data.refreshToken);
            localStorage.setItem('email', email);
            navigate("/Bienvenido");
        }).catch(error => {
            message.error("Credenciales incorrectas.");
        }).finally(() => {
            setLoading(false);
        });

    };

    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);
    };

    return (
        <div className="LoginComponentContainer">
            <Spin spinning={loading}>
                <Card>
                    <div>
                        <h1>PLATAFORMA ADMINISTRATIVA</h1>
                        <h3>INICIO DE SESIÓN</h3>
                    </div>

                    <Form
                        name="basic"
                        layout="vertical"
                        onFinish={onFinish}
                        onFinishFailed={onFinishFailed}
                        autoComplete="off"
                        style={{
                            width: '100%'
                        }}
                    >
                        <Form.Item
                            label="Correo Electrónico: "
                            name="email"
                            rules={[
                                {
                                    required: true,

                                    message: 'Por favor, ingresa un correo electrónico.',
                                }
                            ]}
                        >
                            <Input />
                        </Form.Item>

                        <Form.Item
                            label="Contaseña"
                            name="password"
                            rules={[
                                {
                                    required: true,
                                    message: 'Por favor, ingresa una contraseña.',
                                },
                            ]}
                        >
                            <Input.Password />
                        </Form.Item>

                        <br></br>

                        <Form.Item>
                            <Button type="primary" htmlType="submit">
                                Enviar
                            </Button>
                        </Form.Item>
                    </Form>
                </Card>
            </Spin>
        </div>
    );

};

export default LoginPage;