// src/components/ui/Input.tsx
import React from "react";

interface InputProps {
  label: string;
  className?: string;
}

const Input: React.FC<InputProps> = ({ label, className = "" }) => (
  <div className={`flex flex-col text-sm ${className}`}>
    <label className="mb-1 font-medium">{label}</label>
    <input className="p-2 rounded border border-gray-300" />
  </div>
);

export default Input;
