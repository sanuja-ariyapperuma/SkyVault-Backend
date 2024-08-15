import { useState } from "react";
import { useParams } from "react-router-dom";
import localStyles from "../components/CreateProfile/CustomerProfile.module.css";
import { OptionsType } from "../features/Types/Dashboard/dashboardTypes";
import { useConfirmDialog } from "../hooks/useConfirmDialog";
import ConfirmBox from "../components/CommonComponents/ConfirmBox";
import FrequentFlyNumberAccordion from "../components/CreateProfile/FrequentFlyNumberAccordion";
import ComMethodAccordion from "../components/CreateProfile/ComMethodAccordion";
import PassportAccordion from "../components/CreateProfile/PassportAccordion";
import VISAAccordion from "../components/CreateProfile/VISAAccordion";

const CustomerProfile = () => {
  const { profileId } = useParams();

  const [country, setCountry] = useState<OptionsType[]>([]);
  const { dialogProps, openDialog } = useConfirmDialog();
  const [customerProfileId, setCustomerProfileId] = useState<string | null>("");
  const [primaryPassportId, setPrimaryPassportId] = useState<string | null>("");
  const [secondaryPassportId, setSecondaryPassportId] = useState<string | null>(
    ""
  );

  const setPrimaryPassportSavedId = (id: string) => {
    setPrimaryPassportId(id);
  };
  const setSecondaryPassportSavedId = (id: string) => {
    setSecondaryPassportId(id);
  };

  return (
    <div className={localStyles.profileManagementContainer}>
      <ConfirmBox {...dialogProps} />
      <div className={localStyles.topHeading}>
        <h2 className={localStyles.pageHeader}>
          {profileId ? "Update Profile" : "Create Profile"}
        </h2>
      </div>
      <PassportAccordion
        setCustomerProfileId={setCustomerProfileId}
        customerProfileId={customerProfileId ?? ""}
        setCountry={setCountry}
        country={country}
        setPrimaryPassportSavedId={setPrimaryPassportSavedId}
        setSecondaryPassportSavedId={setSecondaryPassportSavedId}
      />
      <VISAAccordion
        customerProfileId={customerProfileId ?? ""}
        primaryPassportId={primaryPassportId ?? ""}
        secondaryPassportId={secondaryPassportId ?? ""}
        country={country}
        openDialog={openDialog}
      />
      <FrequentFlyNumberAccordion
        CustomerProfileId={customerProfileId ?? ""}
        SystemUser={"9"}
      />
      <ComMethodAccordion
        CustomerProfileId={customerProfileId ?? ""}
        SystemUser="9"
      />
      {/* <div className={localStyles.footerButtonArea}>
        <ButtonPanel OnSave={handleOnSavePrimaryPassport} />
      </div> */}
    </div>
  );
};

export default CustomerProfile;
