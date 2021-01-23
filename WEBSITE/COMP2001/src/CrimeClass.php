<?php

//JSON Object Definition of Data Value
class CrimeClass
{
public $Crime;
public $Year;
    public function __construct($row)
    {
        $arr = preg_split('/,/', $row);
        $this->Crime = $arr[0];
        $this-> Year = $arr[0];
    }

}