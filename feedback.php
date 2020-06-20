<?php
if($_POST["message"]) {
    mail("nusdinomight@gmail.com", "Feedback from site", $_POST["message"], "From: Github pages");
}
?>

<!DOCTYPE html>
<html>
<head>
<title>Feedback/ bug report form</title>
</head>

<body>
  <h1>Submit your feedback/ report a bug here</h1>
  <form method="post" action="feedback.php">
    <label for="fname">Name:</label><br>	
    <input type="text" id="fname" name="fname"><br>	
    <label for="lname">Bug/Feedback:</label><br>	
    <textarea rows="10" cols="30" id="lname" name="lname">	
    </textarea><br><br>	
    <input type="submit" value="Submit">
  </form>
</body>
</html>
