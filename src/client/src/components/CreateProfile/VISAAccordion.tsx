import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Typography,
} from "@mui/material";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";
import ButtonPanel from "../CommonComponents/ButtonPanel";
import VISAInfo from "./VISAInfo";

import customerProfileStyles from "./CustomerProfile.module.css";
import { notifyError, notifySuccess } from "../CommonComponents/Toasters";
import { VISAType } from "../../features/Types/CustomerProfile/VISAType";
import {
  addSingleVISA,
  removeSingleVISA,
  replaceVISAList,
  updateSingle,
} from "../../features/reducers/VISAListReducer";
import { useEffect, useState } from "react";
import {
  convertVisaTypeToSaveVisaType,
  validateVISA,
} from "../../features/Helpers/helper";
import { useDispatch } from "react-redux";
import { OptionsType } from "../../features/Types/Dashboard/dashboardTypes";
import {
  deleteVisaAPI,
  getVisaByCustomerAPI,
  saveVisaAPI,
  updateVisaAPI,
} from "../../features/services/CustomerProfile/apiMethods";

type VISAAccordionProps = {
  customerProfileId: string;
  primaryPassportId: string;
  secondaryPassportId: string;
  country: OptionsType[];
  openDialog(message: string, onConfirm: () => void): void;
};

const VISAAccordion = (props: VISAAccordionProps) => {
  const dispatch = useDispatch();

  const {
    customerProfileId,
    country,
    primaryPassportId,
    secondaryPassportId,
    openDialog,
  } = props;

  const initialVisa: VISAType = {
    id: "",
    visaNumber: "",
    countryId: "",
    issuedPlace: "",
    issuedDate: "",
    expireDate: "",
    assignedToPrimaryPassport: true,
    countryName: "",
    passportNumber: "",
    visaCode: "",
  };

  useEffect(() => {
    if (customerProfileId) {
      fetchVISAs();
    } else {
      dispatch(replaceVISAList([]));
      setVisa(initialVisa);
    }
  }, [customerProfileId]);
  const [visa, setVisa] = useState<VISAType>(initialVisa);
  const handleOnSaveVISA = (): void => {
    if (!customerProfileId) {
      notifyError("Primary passport must be saved first");
      return;
    }

    if (!visa.id) {
      saveVISA(visa);
    } else {
      updateVISA(visa);
    }
  };
  const saveVISA = (visa: VISAType): void => {
    const savingVisa = convertVisaTypeToSaveVisaType(visa);
    savingVisa.CustomerProfileId = customerProfileId;
    savingVisa.PassportId = visa.assignedToPrimaryPassport
      ? primaryPassportId
      : secondaryPassportId;

    const isValid = validateVISA(savingVisa);

    if (!isValid) {
      return;
    }

    saveVisaAPI(savingVisa)
      .then((response) => {
        notifySuccess("VISA added successfully");
        visa.id = response.VisaId;
        visa.countryName =
          country.find((c: OptionsType) => c.value === visa.countryId)?.label ??
          "";
        visa.passportNumber = visa.assignedToPrimaryPassport
          ? primaryPassportId ?? ""
          : secondaryPassportId ?? "";

        dispatch(addSingleVISA(visa));

        setVisa(initialVisa);
      })
      .catch((error) => {
        notifyError(`Sorry! ${error.message}`);
        console.log("Error", error.response);
      });
  };

  const updateVISA = (visa: VISAType): void => {
    const updatingVisa = convertVisaTypeToSaveVisaType(visa);
    updatingVisa.CustomerProfileId = customerProfileId;
    updatingVisa.PassportId = visa.assignedToPrimaryPassport
      ? primaryPassportId
      : secondaryPassportId;

    const isValid = validateVISA(updatingVisa);

    if (!isValid) {
      return;
    }

    updateVisaAPI(visa.id, updatingVisa)
      .then(() => {
        visa.countryName =
          country.find((c: OptionsType) => c.value === visa.countryId)?.label ??
          "";
        visa.passportNumber = visa.assignedToPrimaryPassport
          ? primaryPassportId ?? ""
          : secondaryPassportId ?? "";

        dispatch(updateSingle(visa));
        setVisa(initialVisa);
        notifySuccess("VISA updated successfully");
      })
      .catch((error) => {
        notifyError(`Sorry! ${error.message}`);
        console.log("Error", error.response);
      });
  };
  const handleFieldChangeVISA = (field: string, value: string) => {
    if (
      field === "assignedToPrimaryPassport" &&
      !value &&
      secondaryPassportId === ""
    ) {
      notifyError("Secondary passport must be saved first");
      return;
    }
    setVisa({
      ...visa,
      [field]: value,
    });
  };
  const handleOnVISAEditClick = (editingVisa: VISAType) => {
    console.log("Editing VISA", editingVisa);
    if (editingVisa) {
      setVisa(editingVisa);
    }
  };
  const handleVISADelete = (id: string) => {
    openDialog("Are you sure you want to delete this VISA?", () =>
      deleteVISA(id)
    );
  };
  const deleteVISA = (id: string) => {
    deleteVisaAPI(id)
      .then(() => {
        dispatch(removeSingleVISA(id));
        notifySuccess("VISA deleted successfully");
      })
      .catch((error) => {
        notifyError(`Sorry! ${error.message}`);
        console.log("Error", error.response);
      });
  };
  const fetchVISAs = async () => {
    if (customerProfileId) {
      getVisaByCustomerAPI(customerProfileId).then((visaData) => {
        dispatch(replaceVISAList(visaData));
      });
    }
  };
  return (
    <Accordion>
      <AccordionSummary
        expandIcon={<ArrowDropDownIcon />}
        aria-controls="panel2-content"
        id="panel2-header"
      >
        <Typography className={customerProfileStyles.accordionHeader}>
          <span className={customerProfileStyles.accordionHeader}>
            VISA Information (Optional)
          </span>
        </Typography>
      </AccordionSummary>
      <AccordionDetails>
        {customerProfileId ? (
          <>
            <div className={customerProfileStyles.buttonContainer}>
              <ButtonPanel OnSave={handleOnSaveVISA} />
            </div>
            <VISAInfo
              country={country}
              handleFieldChange={handleFieldChangeVISA}
              OnVISAEditClick={handleOnVISAEditClick}
              initialVisa={visa}
              onVISADeleteClick={handleVISADelete}
            />
          </>
        ) : (
          "Primary passport must be saved before adding VISA"
        )}
      </AccordionDetails>
    </Accordion>
  );
};

export default VISAAccordion;
