import { ComMethodType } from "../../../components/CreateProfile/ComMethodAccordion";
import { FamilyMembersType } from "../../Types/CustomerProfile/CustomerProfileType";
import {
  PassportType,
  SavePassportRequestType,
  SavePassportResponseType,
} from "../../Types/CustomerProfile/PassportType";
import {
  SaveVISAResponseType,
  SaveVISAType,
  VISAType,
} from "../../Types/CustomerProfile/VISAType";
import api from "../api";

export const saveUpdateCustomerProfileAPI = async (
  isUpdate: boolean,
  data: SavePassportRequestType
): Promise<SavePassportResponseType> => {
  const url = isUpdate ? "/UpdatePassport" : "/AddPassport";
  const response = await api.post(url, data);
  return response.data;
};

export const getPassportDataAPI = async (
  profileId: string
): Promise<PassportType[]> => {
  const response = await api.get(
    `/getPassportsByCustomerProfileId/${profileId}`
  );
  return response.data;
};

export const saveVisaAPI = async (
  visa: SaveVISAType
): Promise<SaveVISAResponseType> => {
  const response = await api.post("/AddVISA", visa);
  return response.data;
};

export const updateVisaAPI = async (
  visaId: string,
  visa: SaveVISAType
): Promise<void> => {
  const response = await api.patch(`/UpdateVISA/${visaId}`, visa);
  return response.data;
};

export const deleteVisaAPI = async (visaId: string): Promise<void> => {
  await api.delete(`/DeleteVISA/${visaId}`);
};

export const getVisaByCustomerAPI = async (
  profileId: string
): Promise<VISAType[]> => {
  const response = await api.get(`/getVISAByCustomerProfileId/${profileId}`);
  return response.data;
};

export const getFamilyMembersAPI = async (
  CustomerProfileId: string
): Promise<FamilyMembersType[]> => {
  const response = await api.post<FamilyMembersType[]>(
    `/getFamilyMembers/${CustomerProfileId}`
  );
  return response.data;
};

type FFNListType = {
  FFN: string;
  FFNId?: number;
};

export const getFFNByCustomerAPI = async (
  CustomerProfileId: string
): Promise<FFNListType[]> => {
  const response = await api.get<FFNListType[]>(
    `/getFFNByCustomer/${CustomerProfileId}`
  );
  return response.data;
};

export const updateFFNAPI = async (
  CustomerProfileId: string,
  FFN: string,
  FFNId: number
): Promise<void> => {
  await api.put(`/updateFFN/${FFNId}`, {
    FFN: FFN,
    CustomerProfileId: CustomerProfileId,
  });
};

export const deleteFFNAPI = async (
  FFNId: number,
  CustomerProfileId: string
): Promise<void> => {
  await api.delete(`/deleteFFN/${FFNId}`, {
    data: {
      CustomerProfileId: CustomerProfileId,
    },
  });
};

export const addFFNAPI = async (
  FFN: string,
  CustomerProfileId: string
): Promise<number> => {
  const response = await api.post("/addFFN", {
    FFN: FFN,
    CustomerProfileId: CustomerProfileId,
  });
  return response.data;
};

export const updateComMethodAPI = async (
  CustomerProfileId: string,
  PrefCommId: string,
  WhatsAppNumber: string,
  EmailAddress: string
): Promise<void> => {
  await api.post(`/updateCommMethod`, {
    CustomerProfileId: CustomerProfileId,
    PrefCommId: PrefCommId,
    WhatsAppNumber: WhatsAppNumber,
    EmailAddress: EmailAddress,
  });
};

export const getComMethodAPI = async (
  CustomerProfileId: string
): Promise<ComMethodType> => {
  const response = await api.post(`/getComMethod`, {
    CustomerProfileId: CustomerProfileId,
  });
  return response.data;
};
