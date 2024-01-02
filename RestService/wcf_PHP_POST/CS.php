<?php
echo 'Call the service using GET <br>';
$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, "http://localhost:8314/wcfdemo.svc/GETPersonName?fname=MASTER&lname=POGI");
curl_setopt($ch,CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json','Accept: application/json'));
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);

$result = curl_exec($ch);
$status = curl_getinfo($ch, CURLINFO_HTTP_CODE);

print_r($result);
echo '<br>';

echo '<br>Call the service using POST <br>';
$transmitObject = array("fname" => "MASTER", "lname" => "POGI");
$jsonObject = json_encode($transmitObject);

$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, "http://localhost:8314/wcfdemo.svc/POSTPersonName?");
curl_setopt($ch,CURLOPT_POST, true);
curl_setopt($ch,CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json','Accept: application/json'));
curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonObject);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);

$result = curl_exec($ch);
$status = curl_getinfo($ch, CURLINFO_HTTP_CODE);

curl_close($ch);
print_r($result);
?>