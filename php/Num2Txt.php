<?php

namespace Alp3476;

class Num2Txt
{
    private $textoCentenas = ['uno', 'dos', 'tres', 'cuatro', 'quinientos ', 'seis', 'sete', 'ocho', 'nove'];
    private $textoDecenas = ['uno ', 'veinti', 'treinta ', 'cuarenta ', 'cincuenta ', 'sesenta ', 'setenta ', 'ochenta ', 'noventa '];
    private $textoDiezVeinte = ['diez ', 'once ', 'doce ', 'trece ', 'catorce ', 'quince ', 'dieciseis ', 'diecisiete ', 'dieciocho ', 'diecinueve '];
    private $textoUnidades = ['uno ', 'dos ', 'tres ', 'cuatro ', 'cinco ', 'seis ', 'siete ', 'ocho ', 'nueve '];

    public function toString($numero)
    {
        $valor = floatval($numero);
        if (empty($valor)) {
            return 'cero';
        }
        if ($valor > 999999999999.99) {
            return '';
        }

        $texto = '';

        $esNegativo = $valor < 0;
        if ($esNegativo) {
            $texto .= 'menos ';
            $valor = abs($valor);
        }

        $txtNumero = str_pad(number_format($valor, 2, '.', ''), 15, '0', STR_PAD_LEFT);

        for ($contador = 1; $contador < 6; $contador++) {
            switch ($contador) {
                case 1: $modo = 'm';
                    break;
                case 2: $modo = 'k';
                    break;
                case 3: $modo = 'm';
                    break;
                case 4: $modo = 'c';
                    break;
                case 5: $modo = 'u';
                    break;
            }

            $temp = '';

            if ($contador < 5) {
                $posicion = ($contador - 1) * 3;
                if ($posicion + 3 > strlen($txtNumero)) {
                    $longitud = strlen($txtNumero) - $posicion;
                } else {
                    $longitud = 3;
                }

                $temp = substr($txtNumero, ($contador - 1) * 3, $longitud);
                if ($longitud < 3) {
                    $temp = str_pad($temp, 3, '0');
                }

                $numTemp = intval($temp);
                $c1 = substr($temp, 0, 1);
                $c2 = substr($temp, 1, 1);
                $c3 = substr($temp, 2, 1);
                $texto .= $this->centenas($c1, $c2, $c3);
                $texto .= $this->decenas($c2, $c3);
                $texto .= $this->unidades($c1, $c2, $c3, $modo);
            } else {
                $temp = substr($txtNumero, 13, 2);
                if (!empty($temp)) {
                    $numTemp = intval($temp);
                    if (strlen($temp) < 2) {
                        $temp = str_pad($temp, 2, '0');
                    }

                    $c1 = '0';
                    $c2 = substr($temp, 0, 1);
                    $c3 = substr($temp, 1, 1);
                    $texto .= $this->decenas($c2, $c3);
                    $texto .= $this->unidades($c1, $c2, $c3, $modo);
                }
            }

            if (empty($temp)) {
                continue;
            }

            $numTemp = intval($temp);

            if ($contador == 2 && (strlen($texto) != 0 && !$esNegativo || $esNegativo && strlen($texto) > 6)) {
                $texto .= $c3 == '1' && $c2 == '0' && $c1 == '0' ? 'millón ' : 'millones ';
            }

            if (($contador == 1 || $contador == 3) && $numTemp > 0) {
                $texto .= 'mil ';
            }

            if ($contador == 4 && strlen($txtNumero) >= 13) {
                if (!empty(substr($txtNumero, 13)) && intval(substr($txtNumero, 13)) > 0) {
                    if ($txtNumero[9] == '0' && $txtNumero[10] == '0' && $txtNumero[11] == '1') {
                        $texto .= 'o';
                    } else if (strlen($texto) == 0) {
                        $texto .= 'cero ';
                    }
                    $texto .= 'con ';
                }
            }
        }

        return trim($texto);
    }

    private function centenas($centenas, $decenas, $unidades)
    {
        if ($centenas == '0') {
            return '';
        }

        if ($centenas == '1') {
            if ($decenas == '0' && $unidades == '0') {
                return 'cien ';
            } else {
                return 'ciento ';
            }
        }

        $txt = '';
        for ($contador = 0; $contador <= 9; $contador++) {
            if ($centenas == $contador) {
                $indice = intval($contador) - 1;
                $txt .= $this->textoCentenas[$indice];
                break;
            }
        }

        if ($centenas != '5') {
            $txt .= 'cientos ';
        }

        return $txt;
    }

    private function decenas($decenas, $unidades)
    {
        $txt = '';

        if ($decenas == '0') {
            return '';
        }

        if ($decenas == '1') {
            for ($contador = 0; $contador <= 9; $contador++) {
                if ($unidades == $contador) {
                    $texto = $this->textoDiezVeinte[$contador];
                    break;
                }
            }
            return $texto;
        }

        for ($contador = 1; $contador <= 9; $contador++) {
            if ($contador == $decenas) {
                break;
            }
        }

        if ($contador > 9) {
            $indice = 9;
        } else {
            $indice = $contador - 1;
        }

        if ($unidades == '0') {
            if ($decenas == '2') {
                $txt = 'veinte ';
            } else {
                $txt = $this->textoDecenas[$indice];
            }

            return $txt;
        }

        if ($decenas != '2') {
            $txt = $this->textoDecenas[$indice] . 'y ';
        } elseif ($unidades != '0') {
            $txt = $this->textoDecenas[$indice];
        }

        return $txt;
    }

    private function unidades($centenas, $decenas, $unidades, $modo)
    {
        if ($unidades == '0' || $decenas == '1') {
            return '';
        }

        if ($unidades == '1') {
            if ($decenas == '0' && $centenas == '0') {
                if ($modo == 'm') {
                    return '';
                }
            }

            if ($modo == 'k') {
                return 'un ';
            }
        }

        for ($contador = 1; $contador <= 9; $contador++) {
            if ($contador == $unidades) {
                break;
            }
        }

        if ($contador > '9') {
            $indice = 9;
        } else {
            $indice = $contador - 1;
        }

        return $this->textoUnidades[$indice];
    }
}
