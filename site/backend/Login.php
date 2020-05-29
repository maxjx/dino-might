<?php
$servername = "localhost";
$username = "id13880314_teamcook";
$password = "XCgg8TruMc>0@Fkg";
$dbname = "id13880314_dinomightdb";

$inputUsername = $_POST["inputUsername"];
$inputPassword = $_POST["inputPassword"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully <br>";

// Check the input username and comp
$sql = "SELECT password FROM users WHERE username = '". $inputUsername. "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if ($row["password"] == $inputPassword) {
        echo "Welcome!";
    } else {
        echo "Incorrect password.";
    }
  }
} else {
  echo "Username does not exist.";
}
$conn->close();
?>