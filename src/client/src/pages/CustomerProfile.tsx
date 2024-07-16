import { useParams } from "react-router-dom";

const CustomerProfile = () => {
  const { profileId } = useParams();
  return <div>CustomerProfile {profileId ?? 0}</div>;
};

export default CustomerProfile;
