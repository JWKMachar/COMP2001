<?php


//$fileName = '../assets/Dataset.csv';
$fileName = 'https://plymouth.thedata.place/dataset/85ccae9b-7437-4f4f-9442-dc65453c9f8c/resource/26904f63-e13a-450c-85c7-954b66229871/download/summary-of-all-offences-2003-2015.csv';

if (($handle = fopen($fileName, 'r')) !== FALSE)
{
    $ArrayYear = fgetcsv($handle);
    $string =
        '{ "@context":{"Statistic": "http://schema.org/", "CrimeYear": "http://web.socem.plymouth.ac.uk"}, 
            "Statistic":[
            ';
    $i = 0;
    while (($ArrayRow = fgetcsv($handle, 1000, ",")) !== FALSE) {
        $TEMPstring = "";
        $i++;
        if ( $i == 19)
        {
            for ($x = 1; $x <= 13; $x++) {
                if ($x = 13)
                $TEMPstring = '{
                "@type": "Crime/Year",
                            "Number" : "' . strval($ArrayRow[$x]) . '",
                            "Crime": "' . $ArrayRow[0] . '",
                            "Year": "'. strval($ArrayYear[$x]) . '",
                            "url": "http://www.' . strval($ArrayRow[0]) . strval($ArrayYear[$x]) . '.com"}
                            ]
                            }
                ';
                else{
                    $TEMPstring = '{
                "@type": "Crime/Year",
                            "Number" : "' . strval($ArrayRow[$x]) . '",
                            "Crime": "' . strval($ArrayRow[0]) . '",
                            "Year": "' . strval($ArrayYear[$x]) . '",
                            "url": "http://www.' . strval($ArrayRow[0]) . strval($ArrayYear[$x]) . '.com"},
                ';
                }
                $string = $string . $TEMPstring;
            }
        }
        else
        {
            for ($x = 1; $x <= 13; $x++) {
                $TEMPstring = '{
                "@type": "Crime/Year",
                            "Number" : "' . strval($ArrayRow[$x]) . '",
                            "Crime": "' . strval($ArrayRow[0]) . '",
                            "Year": "' . strval($ArrayYear[$x]) . '",
                            "url": "http://www.' . strval($ArrayRow[0]) . strval($ArrayYear[$x]) . '.com"},
                ';
                $string = $string . $TEMPstring;
            }
        }

    }

}
header('Content-type: application/json');
echo print_r($string);
fclose($handle);

?>
