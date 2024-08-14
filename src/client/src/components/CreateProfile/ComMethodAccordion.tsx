import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  FormControlLabel,
  Radio,
  RadioGroup,
  Typography,
} from "@mui/material";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";

import CustomerProfileStyles from "./CustomerProfile.module.css";
import { useEffect, useState } from "react";
import axios from "axios";
import { baseURL } from "../../features/services/apiCalls";
import { notifyError, notifySuccess } from "../CommonComponents/Toasters";

enum CommunicationMethod {
  None = 1,
  WhatsApp = 2,
  Email = 3,
}

type ComMethodAccordionProps = {
  CustomerProfileId: string;
  SystemUser: string;
};

const ComMethodAccordion = (props: ComMethodAccordionProps) => {
  const { CustomerProfileId, SystemUser } = props;

  const [comMethod, setComMethod] = useState<CommunicationMethod>(
    CommunicationMethod.None
  );

  useEffect(() => {
    getComMethodOnCustomerProfile();
  }, []);

  const onComMethodChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (CustomerProfileId === "") {
      notifyError("Create a passport first to update communication method");
      return;
    }
    if (SystemUser === "") {
      notifyError("Something went wrong. Please try again");
      return;
    }

    updateComMethod(parseInt(e.target.value));
  };

  const updateComMethod = (comMethod: number) => {
    axios
      .post(`${baseURL}/updateCommMethod`, {
        CustomerProfileId: CustomerProfileId,
        SystemUserId: SystemUser,
        PrefCommId: comMethod.toString(),
      })
      .then((response) => {
        console.log(response);
        notifySuccess("Communication method updated successfully");
        setComMethod(comMethod);
      })
      .catch((error) => {
        console.log(error);
        notifyError("Failed to update communication method");
      });
  };

  const getComMethodOnCustomerProfile = () => {
    if (!CustomerProfileId) return;

    axios
      .post(`${baseURL}/getCommMethod`, {
        SystemUserId: SystemUser,
        CustomerProfileId: CustomerProfileId,
      })
      .then((response) => {
        console.log(response);
        setComMethod(parseInt(response.data));
      })
      .catch((error) => {
        console.log(error);
        notifyError("Failed to get communication method");
      });
  };

  return (
    <Accordion>
      <AccordionSummary
        expandIcon={<ArrowDropDownIcon />}
        aria-controls="panel2-content"
        id="panel2-header"
      >
        <Typography className={CustomerProfileStyles.accordionHeader}>
          Preferred Communication Method
        </Typography>
      </AccordionSummary>
      <AccordionDetails>
        <div className={CustomerProfileStyles.accordionContent}>
          <RadioGroup
            defaultValue={CommunicationMethod.None}
            name="radio-buttons-group"
            sx={{ display: "flex", flexDirection: "row" }}
            value={comMethod}
            onChange={onComMethodChange}
          >
            <FormControlLabel
              value={CommunicationMethod.None}
              control={<Radio />}
              label="None"
            />

            <FormControlLabel
              value={CommunicationMethod.WhatsApp}
              control={<Radio />}
              label="WhatsApp"
            />
            <FormControlLabel
              value={CommunicationMethod.Email}
              control={<Radio />}
              label="Email"
            />
          </RadioGroup>
        </div>
      </AccordionDetails>
    </Accordion>
  );
};

export default ComMethodAccordion;
