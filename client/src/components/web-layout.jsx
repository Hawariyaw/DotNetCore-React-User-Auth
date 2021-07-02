import React from "react"

const WebLayout = ({ children }) => {
  return (
    <div
      style={{
        margin: "auto",
        width: "50%",
        paddingTop: "10%",
      }}
    >
      {children}
    </div>
  )
}

export default WebLayout
