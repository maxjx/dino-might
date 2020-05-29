<?php
$servername = "localhost";
$username = "id13880314_teamcook";
$password = "XCgg8TruMc>0@Fkg";
$dbname = "id13880314_dinomightdb";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully <br>";
$sql = "SELECT id, username, password FROM users";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "id: " . $row["id"]. " - Name: " . $row["username"]. " " . $row["password"]. "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();
?>