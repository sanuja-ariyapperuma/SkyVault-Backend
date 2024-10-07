import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import localStyles from "../components/CreateProfile/CustomerProfile.module.css";
import { OptionsType } from "../features/Types/Dashboard/dashboardTypes";
import { useConfirmDialog } from "../hooks/useConfirmDialog";
import ConfirmBox from "../components/CommonComponents/ConfirmBox";
import FrequentFlyNumberAccordion from "../components/CreateProfile/FrequentFlyNumberAccordion";
import ComMethodAccordion from "../components/CreateProfile/ComMethodAccordion";
import PassportAccordion from "../components/CreateProfile/PassportAccordion";
import VISAAccordion from "../components/CreateProfile/VISAAccordion";
import AddIcon from "@mui/icons-material/Add";

import globalStyles from "../components/CommonComponents/Common.module.css";
import FamilyMemberList from "../components/CreateProfile/FamilyMemberList";

const CustomerProfile = () => {
  let { profileId } = useParams();
  const location = useLocation();

  useEffect(() => {
    if (location.state) {
      setCustomerProfileId(location.state.customerProfileId || "");
      setPrimaryPassportId(location.state.primaryPassportId || "");
      setSecondaryPassportId(location.state.secondaryPassportId || "");
    } else if (profileId) {
      setCustomerProfileId(profileId);
    }
  }, [location.state]);

  const [country, setCountry] = useState<OptionsType[]>([]);
  const { dialogProps, openDialog } = useConfirmDialog();
  const [parentId, setParentId] = useState<string | null>("");
  const [customerProfileId, setCustomerProfileId] = useState<string | null>("");
  const [primaryPassportId, setPrimaryPassportId] = useState<string | null>("");
  const [secondaryPassportId, setSecondaryPassportId] = useState<string | null>(
    ""
  );
  const [secondaryPassportEnabled, setSecondaryPassportEnabled] =
    useState<boolean>(true);

  const setPrimaryPassportSavedId = (id: string) => {
    setPrimaryPassportId(id);
    setSecondaryPassportId("");
  };
  const setSecondaryPassportSavedId = (id: string) => {
    setSecondaryPassportId(id);
  };

  const handleOnAddingFamilyMember = () => {
    setParentId(
      parentId ? parentId.toString() : customerProfileId?.toString() ?? "" // parentId.toString() to fix a bug
    );
    setCustomerProfileId("");
    setPrimaryPassportId("");
    setSecondaryPassportId("");
    setSecondaryPassportEnabled(true);
  };

  const handleOnClickMember = (customerId: string) => {
    setCustomerProfileId(customerId.toString());
  };

  return (
    <div className={localStyles.profileManagementContainer}>
      <ConfirmBox {...dialogProps} />
      <div className={localStyles.topHeading}>
        <h2 className={localStyles.pageHeader}>
          {profileId && parentId
            ? "Add Family Member"
            : profileId
            ? "Update Profile"
            : "Create Profile"}
        </h2>
      </div>
      <div className={localStyles.buttonContainer}>
        {customerProfileId && (
          <button
            className={globalStyles.customButton}
            onClick={handleOnAddingFamilyMember}
          >
            <AddIcon className={localStyles.buttonIcon} />
            Add Family Member
          </button>
        )}
      </div>
      <br />
      {customerProfileId ? (
        <>
          <FamilyMemberList
            CustomerProfile={customerProfileId ?? ""}
            ParentId={parentId ?? ""}
            handleOnClickMember={handleOnClickMember}
            setParentId={setParentId}
          />
          <br />
        </>
      ) : (
        <></>
      )}

      <PassportAccordion
        setCustomerProfileId={setCustomerProfileId}
        customerProfileId={customerProfileId ?? ""}
        setCountry={setCountry}
        country={country}
        setPrimaryPassportSavedId={setPrimaryPassportSavedId}
        setSecondaryPassportSavedId={setSecondaryPassportSavedId}
        parentId={parentId ?? ""}
        secondaryPassportEnabled={secondaryPassportEnabled}
        setSecondaryPassportEnabled={setSecondaryPassportEnabled}
      />
      <VISAAccordion
        customerProfileId={customerProfileId ?? ""}
        primaryPassportId={primaryPassportId ?? ""}
        secondaryPassportId={secondaryPassportId ?? ""}
        country={country}
        openDialog={openDialog}
      />
      <FrequentFlyNumberAccordion CustomerProfileId={customerProfileId ?? ""} />
      <ComMethodAccordion CustomerProfileId={customerProfileId ?? ""} />
    </div>
  );
};

export default CustomerProfile;
