<?php
$servername = "localhost";
$username = "id13880314_teamcook";
$password = "XCgg8TruMc>0@Fkg";
$dbname = "id13880314_dinomightdb";

$playerId = $_POST["playerId"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT * FROM loaddata WHERE playerId = '". $playerId. "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  $rows = array();
  while ($row = $result->fetch_assoc()) {
    $rows[] = $row;
  }
  echo json_encode($rows);
} else {
  echo "No saved data found";
}
$conn->close();

?>
