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
import { ChangeEvent, HtmlHTMLAttributes, useEffect, useState } from "react";
import { notifyError, notifySuccess } from "../CommonComponents/Toasters";
import {
  getComMethodAPI,
  updateComMethodAPI,
} from "../../features/services/CustomerProfile/apiMethods";

import globalStyles from "../CommonComponents/Common.module.css";
import ButtonPanel from "../CommonComponents/ButtonPanel";

enum CommunicationMethod {
  None = 1,
  WhatsApp = 3,
  Email = 2,
}

export type ComMethodType = {
  communicationMethod: number;
  whatsAppNumber: string;
  email: string;
};

type ComMethodAccordionProps = {
  CustomerProfileId: string;
};

const ComMethodAccordion = (props: ComMethodAccordionProps) => {
  const { CustomerProfileId } = props;

  const [comMethod, setComMethod] = useState<CommunicationMethod>(
    CommunicationMethod.None
  );

  const [whatsAppNumber, setWhatsAppNumber] = useState<string>("");
  const [emailAddress, setEmailAddress] = useState<string>("");

  useEffect(() => {
    getComMethodOnCustomerProfile();
  }, [CustomerProfileId]);

  const onComMethodChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const chosenMethod = parseInt(e.target.value);
    switch (chosenMethod) {
      case CommunicationMethod.WhatsApp:
        setComMethod(CommunicationMethod.WhatsApp);
        break;
      case CommunicationMethod.Email:
        setComMethod(CommunicationMethod.Email);
        break;
      default:
        setComMethod(CommunicationMethod.None);
        break;
    }
  };

  const isNumeric = (str: string) => /^\d+$/.test(str);
  const isValidEmail = (email: string) => {
    const emailRegex = /^[\w.%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
    return emailRegex.test(email);
  };

  const updateComMethod = () => {
    if (CustomerProfileId === "") {
      notifyError("Create a passport first to update communication method");
      return;
    }

    if (comMethod === CommunicationMethod.Email) {
      if (emailAddress === null || emailAddress.trim() === "") {
        notifyError("Communication method email address cannot be empty");
        return;
      }
      if (!isValidEmail(emailAddress)) {
        notifyError("Invalid Email Address");
        return;
      }
    } else if (comMethod === CommunicationMethod.WhatsApp) {
      if (whatsAppNumber === null || whatsAppNumber.trim() === "") {
        notifyError("Communication method WhatsApp number cannot be empty");
        return;
      }
      if (!isNumeric(whatsAppNumber)) {
        notifyError("WhatsApp Number can only contain numbers");
        return;
      }
    }

    updateComMethodAPI(
      CustomerProfileId,
      comMethod.toString(),
      whatsAppNumber,
      emailAddress
    )
      .then(() => {
        notifySuccess("Communication method updated successfully");
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
          setComMethod(response.communicationMethod);
          setEmailAddress(response.email);
          setWhatsAppNumber(response.whatsAppNumber);
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
            {comMethod !== CommunicationMethod.None && (
              <>
                {comMethod === CommunicationMethod.Email ? (
                  // Input for Email
                  <input
                    type="email"
                    placeholder="Email Address"
                    className={globalStyles.commonTextInput}
                    value={emailAddress}
                    onChange={(event) => setEmailAddress(event.target.value)}
                  />
                ) : (
                  // Input for WhatsApp (or any other method)
                  <input
                    type="telephone"
                    placeholder="WhatsApp Number"
                    className={globalStyles.commonTextInput}
                    value={whatsAppNumber}
                    onChange={(event) => setWhatsAppNumber(event.target.value)}
                  />
                )}
              </>
            )}
            <ButtonPanel OnSave={updateComMethod} />
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
