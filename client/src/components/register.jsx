import React from "react"
import { Form, Input, Button, Alert, notification } from "antd"
import { Link } from "react-router-dom"
function Register() {
  const [registerForm] = Form.useForm()

  const onFinish = async (values) => {
    const { validateFields } = registerForm
    await validateFields()
      .then((values) => {
        fetch("https://localhost:5001/User", {
          method: "POST", // *GET, POST, PUT, DELETE, etc.
          mode: "cors", // no-cors, *cors, same-origin
          cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
          credentials: "same-origin", // include, *same-origin, omit
          headers: {
            "Content-Type": "application/json",
            //   'Content-Type': 'application/x-www-form-urlencoded',
          },
          redirect: "follow", // manual, *follow, error
          referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
          body: JSON.stringify(values), // body data type must match "Content-Type" header
        }).then((response) =>
          notification.success({
            message: "Registration successful!!!",
            onClick: () => {
              console.log("Notification Clicked!")
            },
          }),
        )
      })
      .catch((err) => {
        ;<Alert
          message="Something went wrong! Please try again."
          type="warning"
          closable
        />
      })
  }

  const onFinishFailed = (errorInfo) => {
    return <Alert message="Unable to submit form!" type="error" />
  }

  return (
    <div>
      <h3 className="text-center" style={{ display: "flex" }}>
        .Net Auth with React
      </h3>
      <Form
        form={registerForm}
        name="basic"
        labelCol={{
          span: 8,
        }}
        wrapperCol={{
          span: 16,
        }}
        initialValues={{
          remember: true,
        }}
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
      >
        <Form.Item label="Full Name" name="fullName">
          <Input />
        </Form.Item>
        <Form.Item
          label="Username"
          name="userName"
          rules={[
            {
              required: true,
              message: "Please input your username!",
            },
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Password"
          name="password"
          rules={[
            {
              required: true,
              message: "Please input your password!",
            },
          ]}
        >
          <Input.Password />
        </Form.Item>
        <Form.Item
          wrapperCol={{
            offset: 8,
            span: 16,
          }}
        >
          <Button type="primary" htmlType="submit">
            Submit
          </Button>
        </Form.Item>
      </Form>
      <Link to="/login">
        <span>Goto Login</span>
      </Link>
    </div>
  )
}

export default Register
