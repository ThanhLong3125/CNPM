import React from "react";

interface TextAreaProps {
  label: string;
  className?: string;
}

const TextArea: React.FC<TextAreaProps> = ({ label, className = "" }) => (
  <div className={`flex flex-col text-sm ${className}`}>
    <label className="mb-1 font-medium">{label}</label>
    <textarea className="p-2 rounded border border-gray-300 h-20 resize-none" />
  </div>
);

export default TextArea;
