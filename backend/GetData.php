<?php
$servername = "sql12.freemysqlhosting.net";
$username = "sql12343719";
$password = "ap4cs4pe";

// Create connection
$conn = new mysqli($servername, $username, $password);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully";
?>