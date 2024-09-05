import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";
import Paper from "@mui/material/Paper";
import ModeEditIcon from "@mui/icons-material/ModeEdit";
import DeleteIcon from "@mui/icons-material/Delete";

import CustomerProfileStyles from "./CustomerProfile.module.css";
import globalStyles from "../CommonComponents/Common.module.css";
import { useEffect, useState } from "react";
import ButtonPanel from "../CommonComponents/ButtonPanel";

import localStylea from "./FrequentFlyerNumberAccordion.module.css";
import { notifyError, notifySuccess } from "../CommonComponents/Toasters";
import axios from "axios";
import ConfirmBox from "../CommonComponents/ConfirmBox";
import { useConfirmDialog } from "../../hooks/useConfirmDialog";
import { baseURL } from "../../features/Helpers/helper";
import {
  deleteFFNAPI,
  getFFNByCustomerAPI,
  updateFFNAPI,
} from "../../features/services/CustomerProfile/apiMethods";

type FFNListType = {
  FFN: string;
  FFNId?: number;
};

type FrequentFlyNumberAccordionProps = {
  CustomerProfileId: string;
  SystemUser: string;
};

const FrequentFlyNumberAccordion = (props: FrequentFlyNumberAccordionProps) => {
  const { CustomerProfileId, SystemUser } = props;

  const { dialogProps, openDialog } = useConfirmDialog();

  useEffect(() => {
    if (CustomerProfileId) {
      getAllFFN(CustomerProfileId);
    } else {
      setFFNList([]);
      setFrequentFlyerNumber({ FFN: "" });
    }
  }, [CustomerProfileId]);

  const [fFNList, setFFNList] = useState<FFNListType[]>([]);

  const [frequentFlyerNumber, setFrequentFlyerNumber] = useState<FFNListType>({
    FFN: "",
  });

  const onSave = () => {
    if (frequentFlyerNumber?.FFNId) {
      update();
    } else {
      saveFFN();
    }
  };

  const update = () => {
    if (!validate()) return;
    updateFFNAPI(
      CustomerProfileId,
      frequentFlyerNumber?.FFN,
      frequentFlyerNumber?.FFNId ?? 0
    )
      .then(() => {
        let updatingItem = fFNList.find(
          (ffn) => ffn.FFNId === frequentFlyerNumber?.FFNId
        );

        if (updatingItem) {
          updatingItem.FFN = frequentFlyerNumber?.FFN;
        }

        setFrequentFlyerNumber({ FFN: "", FFNId: undefined });

        notifySuccess("Frequent Flyer Number updated successfully");
      })
      .catch((error) => {
        console.log("Error : ", error);
        notifyError("Failed to save Frequent Flyer Number");
      });
  };

  const saveFFN = () => {
    if (!validate()) return;

    axios
      .post(`${baseURL}/addFFN`, {
        CustomerProfileId: CustomerProfileId,
        FFN: frequentFlyerNumber.FFN,
        SystemUser: SystemUser,
      })
      .then((response) => {
        const newFFNList = [...fFNList];
        newFFNList.push({
          FFN: frequentFlyerNumber?.FFN,
          FFNId: parseInt(response.data),
        });
        setFFNList(newFFNList);
        setFrequentFlyerNumber({ FFN: "" });

        notifySuccess("Frequent Flyer Number saved successfully");
      })
      .catch((error) => {
        console.log("Error : ", error);
        notifyError("Failed to save Frequent Flyer Number");
      });
  };

  const deleteFFN = (id: number) => {
    deleteFFNAPI(id, CustomerProfileId)
      .then(() => {
        notifySuccess("Frequent Flyer Number deleted successfully");

        const newFFNList = fFNList.filter((ffn) => ffn.FFNId !== id);
        setFFNList(newFFNList);
      })
      .catch((error) => {
        console.log("Error : ", error);
        notifyError("Failed to delete Frequent Flyer Number");
      });
  };

  const handleFFNDelete = (id: number | undefined) => {
    if (!id) return;

    openDialog(
      "Are you sure you want to delete this Frequent Flying Number ?",
      () => deleteFFN(id)
    );
  };

  const getAllFFN = (CustomerProfileId: string) => {
    if (!CustomerProfileId) return;

    getFFNByCustomerAPI(CustomerProfileId)
      .then((response) => {
        setFFNList(
          response.map(
            (ffn: any) =>
              ({
                FFN: ffn.ffn,
                FFNId: ffn.ffnId,
              } as FFNListType)
          )
        );
      })
      .catch((error) => {
        console.log("Error : ", error);
        notifyError("Failed to get Frequent Flyer Number");
      });
  };

  const validate = (): boolean => {
    if (frequentFlyerNumber?.FFN === "") {
      notifyError("Frequent Flyer Number is required");
      return false;
    }
    return true;
  };

  const handleEdit = (id: number) => {
    const editingFFN = fFNList.find((ffn) => ffn.FFNId === id);
    if (editingFFN) {
      setFrequentFlyerNumber(editingFFN);
    }
  };

  const handleOnInputChange = (ffn: string) => {
    setFrequentFlyerNumber({
      ...frequentFlyerNumber,
      FFN: ffn,
    });
  };

  return (
    <>
      <ConfirmBox {...dialogProps} />
      <Accordion>
        <AccordionSummary
          expandIcon={<ArrowDropDownIcon />}
          aria-controls="panel2-content"
          id="panel2-header"
        >
          <Typography className={CustomerProfileStyles.accordionHeader}>
            <span className={CustomerProfileStyles.accordionHeader}>
              Frequent Flyer (Optional)
            </span>
          </Typography>
        </AccordionSummary>
        {CustomerProfileId ? (
          <AccordionDetails>
            <div className={CustomerProfileStyles.accordionContent}>
              <div className={CustomerProfileStyles.accordionLeft}>
                <div className={localStylea.frequentFlyerNumberInputArea}>
                  <input
                    type="text"
                    placeholder="Frequent Flyer Number"
                    className={globalStyles.commonTextInput}
                    value={frequentFlyerNumber?.FFN}
                    onChange={(e) => handleOnInputChange(e.target.value)}
                  />
                  <ButtonPanel OnSave={onSave} />
                </div>
                <TableContainer component={Paper}>
                  <Table sx={{ minWidth: 300 }} aria-label="FFN list">
                    <TableHead>
                      <TableRow>
                        <TableCell>Frequent Flyer Number</TableCell>
                        <TableCell>Edit / Delete</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {fFNList.map((FFN) => (
                        <TableRow
                          key={FFN.FFNId}
                          sx={{
                            "&:last-child td, &:last-child th": { border: 0 },
                          }}
                        >
                          <TableCell width={400} component="th" scope="row">
                            {FFN.FFN}
                          </TableCell>
                          <TableCell
                            sx={{ minWidth: 120 }}
                            width={100}
                            scope="row"
                          >
                            {
                              //visas.expireDate && visas.expireDate > today &&
                              <>
                                <button
                                  className={globalStyles.customButtonEdit}
                                  onClick={() =>
                                    handleEdit(FFN.FFNId as number)
                                  }
                                >
                                  <ModeEditIcon />
                                </button>
                                &nbsp;
                                <button
                                  className={globalStyles.customButtonDanger}
                                  onClick={() => handleFFNDelete(FFN.FFNId)}
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
            </div>
          </AccordionDetails>
        ) : (
          <AccordionDetails>
            <Typography className={CustomerProfileStyles.accordionContent}>
              Primary passport must be saved first
            </Typography>
          </AccordionDetails>
        )}
      </Accordion>
    </>
  );
};

export default FrequentFlyNumberAccordion;
