<?php
include_once('header.php')
?>

    <div class="topnav">
        <a href="index.php">Home</a>
        <a href="about.php">About</a>
        <a class="active">Data</a>
    </div>
    <div>
    <h3> </h3>
    </div>
    <div class="text-center">
        <h3 class="text-success">Data Table</h3>
        <?php
            $csv = array_map('str_getcsv', file('../assets/Dataset.csv'));

            if(count($csv) > 0):
                echo "<table class='table table-hover table-bordered table-dark'>";
                echo "<tbody>";
                foreach ($csv as $row): array_map('htmlentities', $row);
                echo "<tr>";
                echo "<th> <p class='text-success'>" .implode('</p></th><td><p class="text-light">', $row). "</p></td>";
                echo "</td>";
                echo "</tr>";
                endforeach;
                echo "</tbody>";
                echo "</table>";

            endif;
        ?>
    </div>

<?php
include_once('footer.php')
?>