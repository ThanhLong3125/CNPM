// Utility functions để format dữ liệu

// Chuyển đổi giới tính từ English sang Tiếng Việt
export const mapGender = (gender: string): string => {
  if (!gender) return "";
  
  const lowerGender = gender.toLowerCase().trim();
  
  if (lowerGender === "male") return "Nam";
  if (lowerGender === "female") return "Nữ";
  
  // Nếu đã là "Nam" hoặc "Nữ" thì giữ nguyên
  if (lowerGender === "nam" || lowerGender === "nữ") {
    return gender.charAt(0).toUpperCase() + gender.slice(1).toLowerCase();
  }
  
  return gender; // Trả về giá trị gốc nếu không match
};

// Format datetime
export const formatDateTime = (isoDate: string): string => {
  if (!isoDate) return "";
  
  const date = new Date(isoDate);
  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const year = date.getFullYear();
  const hours = String(date.getHours()).padStart(2, "0");
  const minutes = String(date.getMinutes()).padStart(2, "0");
  
  return `${day}/${month}/${year} ${hours}:${minutes}`;
};

// Format date only
export const formatDate = (isoDate: string): string => {
  if (!isoDate) return "";
  
  const date = new Date(isoDate);
  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const year = date.getFullYear();
  
  return `${day}/${month}/${year}`;
};

// Reverse mapping - chuyển từ Tiếng Việt sang English (cho API calls)
export const reverseMapGender = (gender: string): string => {
  if (!gender) return "";
  
  const trimmedGender = gender.trim();
  
  if (trimmedGender === "Nam") return "male";
  if (trimmedGender === "Nữ") return "female";
  
  return gender.toLowerCase(); // Trả về lowercase nếu không match
}; 