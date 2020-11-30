<?php
$servername = "localhost";
$username = "id15545090_teamcook";
$password = "6Mfn&9[zT2m~sWP+";
$dbname = "id15545090_dinomight";

$inputUsername = $_POST["inputUsername"];
$inputPassword = $_POST["inputPassword"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

// Check the input username and comp
$sql = "SELECT password, id FROM users WHERE username = '". $inputUsername. "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if ($row["password"] == $inputPassword) {
        echo $row["id"];
    } else {
        echo "Incorrect password.";
    }
  }
} else {
  echo "Username does not exist.";
}
$conn->close();
?>