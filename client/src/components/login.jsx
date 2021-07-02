import React, { useState } from "react"
import { Form, Input, Button, notification } from "antd"
import { Link, useHistory } from "react-router-dom"

function Login() {
  const [loginForm] = Form.useForm()
  const history = useHistory()
  const [error] = useState("")
  const onFinish = async (values) => {
    console.log(values)
    const { validateFields } = loginForm
    await validateFields()
      .then((values) => {
        fetch("https://localhost:5001/Login", {
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
        })
          .then((response) => response.json())
          .then((response) => {
            console.log(response)
            localStorage.setItem("auth-token", response.token)
            localStorage.setItem("userFullName", response.fullName)
            response?.id
              ? history.push("/dashboard")
              : notification.error({
                  message: response.message,
                  onClick: () => {
                    console.log("Notification Clicked!")
                  },
                })
          })
          .catch((err) => {
            console.log(err)
          })
      })
      .catch((err) => {
        notification.error({
          message: err.message,
          onClick: () => {
            console.log("Notification Clicked!")
          },
        })
      })
  }
  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo)
  }
  return (
    <div>
      <h3 className="text-center" style={{ display: "flex" }}>
        .Net Auth with React
      </h3>
      <Form
        form={loginForm}
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
        <Form.Item
          label="Username"
          name="userName"
          rules={[
            {
              required: true,
              message: "Please input your email!",
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
        <div style={{ color: "red" }}>{error ? error : ""}</div>
      </Form>
      <Link to="/register">
        <span>Goto Signup</span>
      </Link>
    </div>
  )
}

export default Login
