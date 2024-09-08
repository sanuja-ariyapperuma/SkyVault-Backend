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
import { notifyError, notifySuccess } from "../CommonComponents/Toasters";
import { baseURL } from "../../features/Helpers/helper";
import {
  getComMethodAPI,
  updateComMethodAPI,
} from "../../features/services/CustomerProfile/apiMethods";

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
  }, [CustomerProfileId]);

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
    updateComMethodAPI(CustomerProfileId, comMethod.toString())
      .then(() => {
        notifySuccess("Communication method updated successfully");
        setComMethod(comMethod);
      })
      .catch((error) => {
        console.log(error);
        notifyError("Failed to update communication method");
      });
  };

  const getComMethodOnCustomerProfile = () => {
    if (CustomerProfileId) {
      getComMethodAPI(CustomerProfileId)
        .then((response) => {
          setComMethod(response);
        })
        .catch((error) => {
          console.log(error);
          notifyError("Failed to get communication method");
        });
    } else {
      setComMethod(CommunicationMethod.None);
    }
  };

  return (
    <Accordion>
      <AccordionSummary
        expandIcon={<ArrowDropDownIcon />}
        aria-controls="panel2-content"
        id="panel2-header"
      >
        <Typography className={CustomerProfileStyles.accordionHeader}>
          <span className={CustomerProfileStyles.accordionHeader}>
            Preferred Communication Method
          </span>
        </Typography>
      </AccordionSummary>
      {CustomerProfileId ? (
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
      ) : (
        <AccordionDetails>
          <Typography className={CustomerProfileStyles.accordionContent}>
            Primary passport must be saved first
          </Typography>
        </AccordionDetails>
      )}
    </Accordion>
  );
};

export default ComMethodAccordion;
