<?php
$servername = "localhost";
$username = "id13880314_teamcook";
$password = "XCgg8TruMc>0@Fkg";
$dbname = "id13880314_dinomightdb";

$playerId = $_POST["playerId"];
$playerLevel = $_POST["playerLevel"];
$playerHealth = $_POST["playerHealth"];
$Xcoordinate = $_POST["Xcoordinate"];
$Ycoordinate = $_POST["Ycoordinate"];
$playerMaxHealth = $_POST["playerMaxHealth"];
$kickDmg = $_POST["kickDmg"];
$fireballDmg = $_POST["fireballDmg"];
$canDash = $_POST["canDash"];
$questNumber = $_POST["questNumber"];
$kingSpared = $_POST["kingSpared"];
$masterSpared = $_POST["masterSpared"];
$choices = $_POST["choices"];
$NPCCanvasDict = $_POST["NPCCanvasDict"];
$NPCDialogueDict = $_POST["NPCDialogueDict"];



$conn = new mysqli($servername, $username, $password, $dbname);$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT playerId FROM loaddata where playerId = '". $playerId. "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    $sql = "DELETE FROM loaddata WHERE playerId = '". $playerId. "';";
    if ($conn->query($sql) === true) {
        $sql = "INSERT INTO loaddata (playerId, level, health, Xcoordinate, Ycoordinate, playerMaxHealth
        , kickDmg, fireballDmg, canDash, questNumber, kingSpared, masterSpared, choices, NPCCanvasDict
        , NPCDialogueDict)
        VALUES ('". $playerId. "', '". $playerLevel. "', '". $playerHealth. "', '". $Xcoordinate. "', '". $Ycoordinate. "'
        , '". $playerMaxHealth. "', '". $kickDmg. "', '". $fireballDmg. "', '". $canDash. "', '". $questNumber. "'
        , '". $kingSpared. "', '". $masterSpared. "', '". $choices. "', '". $NPCCanvasDict. "', '". $NPCDialogueDict. "'
        )";
        if ($conn->query($sql) === true) {
            echo "Successful update!";
        } else {
            echo "error";
        }
    }
} else {
    $sql = "INSERT INTO loaddata (playerId, level, health, Xcoordinate, Ycoordinate, playerMaxHealth
        , kickDmg, fireballDmg, canDash, questNumber, kingSpared, masterSpared, choices, NPCCanvasDict
        , NPCDialogueDict)
        VALUES ('". $playerId. "', '". $playerLevel. "', '". $playerHealth. "', '". $Xcoordinate. "', '". $Ycoordinate. "'
        , '". $playerMaxHealth. "', '". $kickDmg. "', '". $fireballDmg. "', '". $canDash. "', '". $questNumber. "'
        , '". $kingSpared. "', '". $masterSpared. "', '". $choices. "', '". $NPCCanvasDict. "', '". $NPCDialogueDict. "'
        )";
    if ($conn->query($sql) === true) {
        echo "Successful save!";
    } else {
        echo "Initial save failed.";
    }
}
$conn->close();
?>
