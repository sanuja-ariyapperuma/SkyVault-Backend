import api from "../api";

type SearchResponse = {
  query: string;
  profiles: Profile[];
};

type Profile = {
  profileId: string;
  lastName: string;
  otherNames: string;
  passportNumber: string;
  salutation: string;
};

export const searchProfiles = async (
  searchQuery: string
): Promise<SearchResponse> => {
  const response = await api.post("/searchprofile", {
    SearchQuery: searchQuery,
  });
  return response.data;
};
