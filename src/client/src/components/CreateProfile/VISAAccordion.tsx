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
import axios from "axios";
import {
  SaveVISAResponseType,
  VISAType,
} from "../../features/Types/CustomerProfile/VISAType";
import { baseURL } from "../../features/services/apiCalls";
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
    issuedDate: null,
    expireDate: null,
    assignedToPrimaryPassport: true,
    countryName: "",
    passportNumber: "",
  };

  useEffect(() => {
    console.log("Customer Profile ID VISA ", customerProfileId);
    if (customerProfileId) {
      fetchVISAs();
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

    axios
      .post<SaveVISAResponseType>(`${baseURL}/AddVISA`, savingVisa)
      .then((response) => {
        notifySuccess("VISA added successfully");

        visa.id = response.data.VisaId;
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

    axios
      .put<SaveVISAResponseType>(
        `${baseURL}/updatevisa/${visa.id}`,
        updatingVisa
      )
      .then((response) => {
        if (response.status == 200) {
          visa.countryName =
            country.find((c: OptionsType) => c.value === visa.countryId)
              ?.label ?? "";
          visa.passportNumber = visa.assignedToPrimaryPassport
            ? primaryPassportId ?? ""
            : secondaryPassportId ?? "";

          dispatch(updateSingle(visa));

          setVisa(initialVisa);

          notifySuccess("VISA updated successfully");
        } else {
          notifyError(
            "Something went wrong. VISA did not updated successfully"
          );
        }
      })
      .catch((error) => {
        notifyError(`Sorry! ${error.message}`);
        console.log("Error", error.response);
      });
  };
  const handleFieldChangeVISA = (field: string, value: any) => {
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
    axios
      .delete(`${baseURL}/deletevisa/${id}`, {
        data: { SystemUserId: "9", CustomerProfileId: customerProfileId },
      })
      .then((response) => {
        if (response.status == 200) {
          dispatch(removeSingleVISA(id));
          notifySuccess("VISA deleted successfully");
        } else {
          notifyError("Something went wrong. VISA did not delete successfully");
        }
      })
      .catch((error) => {
        notifyError(`Sorry! ${error.message}`);
        console.log("Error", error.response);
      });
  };
  const fetchVISAs = async () => {
    if (!customerProfileId) {
      return;
    }

    try {
      const visaData = await axios.post<VISAType[]>(
        `${baseURL}/getVISAByCustomer`,
        {
          CustomerProfileId: customerProfileId,
          SystemUserId: "9",
        }
      );

      dispatch(replaceVISAList(visaData.data));
    } catch (error) {
      console.log("Error", error);
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
          VISA Information (Optional)
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
          "Primary passport must be saved first"
        )}
      </AccordionDetails>
    </Accordion>
  );
};

export default VISAAccordion;
