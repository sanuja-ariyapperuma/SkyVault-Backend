import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";

import globalStyles from "../CommonComponents/Common.module.css";
import ModeEditIcon from "@mui/icons-material/ModeEdit";
import DeleteIcon from "@mui/icons-material/Delete";
import { useSelector } from "react-redux";
import { RootState } from "../../store";
import { VISAType } from "../../features/Types/CustomerProfile/VISAType";

type VISAListProps = {
  OnEditClick: (editingVisa: VISAType) => void;
  OnDeleteClick: (id: string) => void;
};

const VISAList = (props: VISAListProps) => {
  const { OnEditClick, OnDeleteClick } = props;

  const { visas } = useSelector((state: RootState) => state.visaListR);

  const handleVisaEditClick = (id: string) => {
    const editingVisa = visas.find((visa) => visa.id === id);
    OnEditClick(editingVisa!);
  };

  const handleVisaDeleteClick = (id: string) => {
    OnDeleteClick(id);
  };

  return (
    <div>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="visa list">
          <TableHead>
            <TableRow>
              <TableCell>VISA Number</TableCell>
              <TableCell align="right">Country</TableCell>
              <TableCell align="right">VISA Issued Date</TableCell>
              <TableCell align="right">VISA Expiry Date</TableCell>
              <TableCell align="right">Assigned Passport</TableCell>
              <TableCell align="right">Edit / Delete</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {visas.length > 0 &&
              visas.map((visa) => (
                <TableRow
                  key={visa.id}
                  sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                >
                  <TableCell component="th" scope="row">
                    {visa.visaNumber}
                  </TableCell>
                  <TableCell align="right">{visa.countryName}</TableCell>
                  <TableCell align="right">
                    {visa.issuedDate?.toString() ?? ""}
                  </TableCell>
                  <TableCell align="right">
                    {visa.expireDate?.toString() ?? ""}
                  </TableCell>
                  <TableCell align="right">
                    {visa.assignedToPrimaryPassport
                      ? "Primary Passport"
                      : "Secondary Passport"}
                  </TableCell>
                  <TableCell align="right">
                    {
                      //visas.expireDate && visas.expireDate > today &&
                      <>
                        <button
                          className={globalStyles.customButtonEdit}
                          onClick={() => handleVisaEditClick(visa.id)}
                        >
                          <ModeEditIcon />
                        </button>
                        &nbsp;
                        <button
                          className={globalStyles.customButtonDanger}
                          onClick={() => handleVisaDeleteClick(visa.id)}
                        >
                          <DeleteIcon />
                        </button>
                      </>
                    }
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
