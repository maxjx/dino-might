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
echo "Registration connected successfully <br>";

// Check the input username and comp
$sql = "SELECT username FROM users WHERE username = '". $inputUsername. "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // Since result is greater than 0, at least 1 such username already exists
    echo "Username already taken.";
} else {
    $sql = "INSERT INTO users (username, password)
        VALUES ('". $inputUsername . "', '" . $inputPassword . "')";
    if ($conn->query($sql) === TRUE) {
        echo "New username created successfully";
      } else {
        echo "Error: " . $sql . "<br>" . $conn->error;
      }
}
$conn->close();
?>