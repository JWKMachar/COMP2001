<?php
require_once("../src/CrimeView.php");
$crimes = new CrimeView();
$json = (object)array("Crimes" =>$crimes->showJson());
header('Content-type: application/json');
echo json_encode($json, JSON_PRETTY_PRINT);

?>