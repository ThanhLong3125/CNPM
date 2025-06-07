import React from "react";
import a4 from "../../assets/a4.png";
import a5 from "../../assets/a5.png";

const Footer: React.FC = () => {
    return (
        <footer className=" text-white py-10 px-12 gap-8" style={{ backgroundImage: `url(${a5})`, backgroundSize: 'cover' }}>
            <div className="flex flex-col lg:flex-row lg:items-center gap-10 top-2">
                <div className="lg:w-1/3 items-start">
                    <img src={a4} alt="Footer Image" className="w-full h-auto object-cover mb-4 lg:mb-0" />
                </div>
                <div className="lg:w-2/3 text-center">
                    <div className="grid grid-cols-3 gap-6">
                        <div>
                            <h2 className="text-2xl font-bold mb-4">Company</h2>
                            <ul className="list-none space-y-2">
                                <li>About</li>
                                <li>News</li>
                                <li>Blog</li>
                                <li>FAQ</li>
                                <li>Plans</li>
                                <li>Privacy Policy</li>
                                <li>Terms of Service</li>
                                <a href="https://youtube.com" target="_blank" rel="noopener noreferrer">Youtube</a> <br />
                                <a href="https://facebook.com" target="_blank" rel="noopener noreferrer">Facebook</a> <br />
                                <a href="contact">Contact</a>
                            </ul>
                        </div>
                        <div className="text-center">
                            <h2 className="text-2xl font-bold mb-4">Image Tools</h2>
                            <ul className="list-none space-y-2">
                                <li>Import DICOM Images</li>
                                <li>AI-Based Image Classification</li>
                                <li>Compare Images Over Time</li>
                                <li>Image Annotation Support</li>
                                <li>View & Adjust Images (Zoom, Pan, Contrast)</li>
                            </ul>       
                            </div>
                        <div className="text-center break-words">
                            <h2 className="text-2xl font-bold mb-4">AI & Diagnostic Features</h2>
                            <ul className="list-none space-y-2">
                                <li>Auto Disease Detection from Images</li>
                                <li>Receive AI Diagnostic Suggestions</li>
                                <li>Prioritized Case Notification</li>
                                <li>Physician Review Reminder</li>
                                <li>Diagnostic Accuracy Statistics</li>
                                <li>Strong AI Model Integration</li>
                                <li>Compare AI Results with Past Data</li>
                                <li>Annotate & Note on AI Outputs</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </footer>

    );
};
export default Footer;