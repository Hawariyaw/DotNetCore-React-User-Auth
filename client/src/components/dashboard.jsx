import React, { useEffect, useState } from "react"
import { Button, Table } from "antd"
import { Link } from "react-router-dom"

function Dashboard() {
  const [allUsers, setAllUsers] = useState([])
  const [fullName] = useState(localStorage.getItem("userFullName") || "Default")
  const deleteUser = (value) => {
    const token = localStorage.getItem("auth-token")
    fetch("https://localhost:5001/User/" + value, {
      method: "DELETE", // *GET, POST, PUT, DELETE, etc.
      mode: "cors", // no-cors, *cors, same-origin
      cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
      credentials: "same-origin", // include, *same-origin, omit
      headers: {
        "Content-Type": "application/json",
        authorization: `Bearer ${token}`,
      },
      redirect: "follow", // manual, *follow, error
      referrerPolicy: "no-referrer",
    })
      .then((response) => {
        const _allUsers = allUsers.filter((user) => {
          return user.id !== value
        })
        console.log(_allUsers)
        setAllUsers(_allUsers)
      })
      .catch((err) => {
        console.log(err)
      })
  }

  const columns = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
      render: (text) => <span>{text}</span>,
    },
    {
      title: "Full Name",
      dataIndex: "fullName",
      key: "fullName",
      render: (text) => <span>{text}</span>,
    },
    {
      title: "Email",
      dataIndex: "userName",
      key: "userName",
    },
    {
      title: "",
      dataIndex: "",
      key: "x",
      render: (record) => (
        <Button type="link" onClick={() => deleteUser(record.id)}>
          Delete
        </Button>
      ),
    },
  ]
  useEffect(() => {
    const token = localStorage.getItem("auth-token")
    fetch("https://localhost:5001/User", {
      method: "GET", // *GET, POST, PUT, DELETE, etc.
      mode: "cors", // no-cors, *cors, same-origin
      cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
      credentials: "same-origin", // include, *same-origin, omit
      headers: {
        "Content-Type": "application/json",
        authorization: `Bearer ${token}`,
      },
      redirect: "follow", // manual, *follow, error
      referrerPolicy: "no-referrer",
    })
      .then((response) => response.json())
      .then((values) => setAllUsers(values))
  }, [])

  return (
    <div>
      <div className="" style={{ float: "right" }}>
        <Link to="/login">
          <Button>Logout</Button>
        </Link>
      </div>
      <h1>Welcome to Users Admin Dashboard, {fullName.toUpperCase()}</h1>
      <div className="">
        <Table columns={columns} dataSource={allUsers} />
      </div>
    </div>
  )
}

export default Dashboard
