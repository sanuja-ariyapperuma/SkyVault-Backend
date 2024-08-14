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
  SaveVISAType,
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
import { validateVISA } from "../../features/Helpers/helper";
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
  useEffect(() => {
    if (customerProfileId) {
      fetchVISAs();
    }
  }, []);
  const {
    customerProfileId,
    country,
    primaryPassportId,
    secondaryPassportId,
    openDialog,
  } = props;
  const [visa, setVisa] = useState<VISAType>({
    id: "",
    visaNumber: "",
    countryId: "",
    issuedPlace: "",
    issuedDate: null,
    expireDate: null,
    assignedToPrimaryPassport: true,
    countryName: "",
    passportNumber: "",
  });
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
    const saveVisa = {
      VisaNumber: visa.visaNumber,
      CountryId: visa.countryId,
      IssuedPlace: visa.issuedPlace,
      IssuedDate: visa.issuedDate,
      ExpiryDate: visa.expireDate,
      CustomerProfileId: customerProfileId,
      SystemUserId: "9",
      PassportId: visa.assignedToPrimaryPassport
        ? primaryPassportId
        : secondaryPassportId,
    } as SaveVISAType;

    const isValid = validateVISA(saveVisa);

    if (!isValid) {
      return;
    }

    axios
      .post<SaveVISAResponseType>(`${baseURL}/AddVISA`, saveVisa)
      .then((response) => {
        console.log("Response", response);
        notifySuccess("VISA added successfully");

        visa.id = response.data.VisaId;
        visa.countryName =
          country.find((c: OptionsType) => c.value === visa.countryId)?.label ??
          "";
        visa.passportNumber = visa.assignedToPrimaryPassport
          ? primaryPassportId ?? ""
          : secondaryPassportId ?? "";

        dispatch(addSingleVISA(visa));

        setVisa({
          id: "",
          visaNumber: "",
          countryId: "",
          issuedPlace: "",
          issuedDate: null,
          expireDate: null,
          assignedToPrimaryPassport: true,
          countryName: "",
          passportNumber: "",
        });
      })
      .catch((error) => {
        notifyError(`Sorry! ${error.message}`);
        console.log("Error", error.response);
      });
  };
  const updateVISA = (visa: VISAType): void => {
    const saveVisa = {
      VisaNumber: visa.visaNumber,
      CountryId: visa.countryId,
      IssuedPlace: visa.issuedPlace,
      IssuedDate: visa.issuedDate,
      ExpiryDate: visa.expireDate,
      CustomerProfileId: customerProfileId,
      SystemUserId: "9",
      PassportId: visa.assignedToPrimaryPassport
        ? primaryPassportId
        : secondaryPassportId,
    } as SaveVISAType;

    const isValid = validateVISA(saveVisa);

    if (!isValid) {
      return;
    }

    axios
      .put<SaveVISAResponseType>(`${baseURL}/updatevisa/${visa.id}`, saveVisa)
      .then((response) => {
        if (response.status == 200) {
          visa.countryName =
            country.find((c: OptionsType) => c.value === visa.countryId)
              ?.label ?? "";
          visa.passportNumber = visa.assignedToPrimaryPassport
            ? primaryPassportId ?? ""
            : secondaryPassportId ?? "";

          dispatch(updateSingle(visa));

          setVisa({
            id: "",
            visaNumber: "",
            countryId: "",
            issuedPlace: "",
            issuedDate: null,
            expireDate: null,
            assignedToPrimaryPassport: true,
            countryName: "",
            passportNumber: "",
          });

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
    console.log("Profile ID", customerProfileId);
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
