import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";

const VISAList = () => {
  const createData = (
    visaNumber: string,
    countryName: string,
    expiryDate: string,
    assignedPassportNumber: string
  ) => {
    return { visaNumber, countryName, expiryDate, assignedPassportNumber };
  };

  const rows = [
    createData("A1234322333223", "Finland", "2023-12-12", "N98767898"),
    createData("A1234322338888", "Netherlands", "2023-12-12", "N98767898"),
  ];

  return (
    <div>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>VISA Number</TableCell>
              <TableCell align="right">Country</TableCell>
              <TableCell align="right">VISA Expiry Date</TableCell>
              <TableCell align="right">Assigned Passport Number</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map((row) => (
              <TableRow
                key={row.visaNumber}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {row.visaNumber}
                </TableCell>
                <TableCell align="right">{row.countryName}</TableCell>
                <TableCell align="right">{row.expiryDate}</TableCell>
                <TableCell align="right">
                  {row.assignedPassportNumber}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};

export default VISAList;
