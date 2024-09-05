import { ProfileDefinitionResponse } from "../../../Types/CustomerProfile/CommonDefinitions";
import api from "../../api";

export const fetchCommonDataAPI =
  async (): Promise<ProfileDefinitionResponse> => {
    const response = await api.post("/customerProfileCommonData");
    return response.data;
  };
