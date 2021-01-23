<?php
require_once("CrimeModel.php");

class CrimeView extends CrimeModel
{
    public function showJson()
    {
        $data = $this->getData();
        $array = array();

        //LOOP THROUGH ARRAY AND ADD JSON FORMATTING
        foreach ($data as $row):
            array_push($data, (object)array(
                "@type" => "Crime/Year",
                "CrimeCount" => array(
                    "@type" => "CrimeCount",
                    "Crime" => $row->Crime,
                    "Year" => $row->Year,
                )));
        endforeach;
        return $array;
    }
}