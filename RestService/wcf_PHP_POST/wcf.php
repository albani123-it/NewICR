<?php
	echo '<br>Call the service using POST <br>';
$transmitObject = array("fname" => "MASTER", "lname" => "IQBAL");
$jsonObject = json_encode($transmitObject);
 
$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, "http://localhost:54238/NProspect.svc/json/New?");
curl_setopt($ch,CURLOPT_POST, true);
curl_setopt($ch,CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json','Accept: application/json'));
curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonObject);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);
 
$result = curl_exec($ch);
$status = curl_getinfo($ch, CURLINFO_HTTP_CODE);
 
curl_close($ch);
print_r($ch);
print_r($result);
?>